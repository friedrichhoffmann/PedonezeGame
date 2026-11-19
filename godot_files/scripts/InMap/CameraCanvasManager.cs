using Godot;
using System;
using System.Threading.Tasks;
using EventSystem;
using static EventSystem.EventHandler;

public partial class CameraCanvasManager : Node
{
    [Export] public NodePath BookPath { get; set; } = "CanvasLayer/UI/Book";

    private Control Book = null!;
    private AnimationPlayer BookPageAnim = null!;
    private AnimationPlayer BookHelpAnim = null!;

    private static readonly Func<Node, Event> ToggleVis = ctrl => new Event(
        "Visibility Toggle",
        EventKind.ToggleVisibility,
        EventBody.ToggleVisibility(ctrl)
    );

    private static readonly Func<AnimationPlayer, string, int, Event> PlayAnimMS =
        (anim, name, ms) => new Event(
            "Play animation",
            EventKind.Animation,
            EventBody.Animation(anim, name, ms)
        );

    private static readonly Func<AnimationPlayer, string, Event> PlayAnim =
        (anim, name) => new Event(
            "Play animation",
            EventKind.Animation,
            EventBody.Animation(anim, name)
        );

    public override void _Ready()
    {
        Book = GetNode<Control>(BookPath);
        BookPageAnim = Book.GetNode<AnimationPlayer>("AnimationPlayer");
        BookHelpAnim = Book.GetNode<AnimationPlayer>("HelpMenuPlayer");

        _ = PrepareBookLayout();
    }

    public override void _Input(InputEvent @event) {
        if (@event is not InputEventKey keyEvent ||
                !keyEvent.Pressed ||
                keyEvent.Echo)
        {
            return;
        }

        bool shouldToggle =
            (!Book.Visible && keyEvent.Keycode == Key.Q) ||
            (Book.Visible && keyEvent.Keycode == Key.Escape);

        if (!shouldToggle)
            return;

        ToggleBook();
        GetViewport().SetInputAsHandled();
    }

    private async Task PrepareBookLayout()
    {
        Color originalModulate = Book.Modulate;

        Book.Visible = true;
        Book.Modulate = new Color(
            originalModulate.R,
            originalModulate.G,
            originalModulate.B,
            0f
        );

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        Book.Visible = false;
        Book.Modulate = originalModulate;
    }

    private void ToggleBook()
    {
        if (!Book.Visible)
        {
            ThrowEvent(PlayAnimMS(BookPageAnim, "OpenBook", 0));
            ThrowEvent(PlayAnimMS(BookHelpAnim, "InThenOutHelpMenu", 0));
            ThrowEvent(ToggleVis(Book));
        }
        else
        {
            ThrowEvent(PlayAnim(BookPageAnim, "CloseBook"));
            ThrowEvent(ToggleVis(Book));
        }
    }
}


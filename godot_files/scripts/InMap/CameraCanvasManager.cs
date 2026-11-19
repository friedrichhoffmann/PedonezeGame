
using Godot;
using System;
using System.Threading.Tasks;
using EventSystem;
using static EventSystem.EventHandler;
using CombatAPI;

public partial class CameraCanvasManager : Node
{
	[Export] public NodePath BookPath { get; set; } = "CanvasLayer/UI/Book";
	[Export] public NodePath CombatPath { get; set; } = "CanvasLayer/UI/Battle";

	public Control Combat = null!;
	private Control Book = null!;
	private AnimationPlayer localAnimationPlayer = null!;
	private AnimationPlayer bookPageAnim = null!;
	private AnimationPlayer bookHelpAnim = null!;

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

	public Event FadeIn;
	public Event FadeOut;

	public override void _Ready()
	{
		Book = GetNode<Control>(BookPath);
		bookPageAnim = Book.GetNode<AnimationPlayer>("AnimationPlayer");
		bookHelpAnim = Book.GetNode<AnimationPlayer>("HelpMenuPlayer");

		localAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		FadeIn = new Event(
			"Play animation",
			EventKind.Animation,
			EventBody.Animation(localAnimationPlayer, "FadeIn")
		);

		FadeOut = new Event(
			"Play animation",
			EventKind.Animation,
			EventBody.Animation(localAnimationPlayer, "FadeOut")
		);

		Combat = GetNode<Control>(CombatPath);
		Combat.Hide();

		_ = PrepareBookLayout();
	}

	public override void _Input(InputEvent @event)
	{
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
			ThrowEvent(PlayAnimMS(bookPageAnim, "OpenBook", 0));
			ThrowEvent(PlayAnimMS(bookHelpAnim, "InThenOutHelpMenu", 0));
			ThrowEvent(ToggleVis(Book));
		}
		else
		{
			ThrowEvent(PlayAnim(bookPageAnim, "CloseBook"));
			ThrowEvent(ToggleVis(Book));
		}
	}
    public async Task ShowCombat(Battle battle, Node invokingEnemy) {
        Book.Hide();
        ThrowEvent(FadeIn);

        foreach (Node node in invokingEnemy.GetParent().GetChildren())
            node.QueueFree();

        ThrowEvent(ToggleVis(Combat));
        ThrowEvent(FadeOut);

        bool heroesWon = await battle.StartBattle();

        ThrowEvent(FadeIn);
        ThrowEvent(ToggleVis(Combat));
        ThrowEvent(FadeOut);

        if (!heroesWon)
        {
            string pathToGameOverScene = "res://game_over.tscn";
            GetTree().ChangeSceneToFile(pathToGameOverScene);
        }
    }

}

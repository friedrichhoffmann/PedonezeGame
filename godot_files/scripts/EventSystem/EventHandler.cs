using Godot;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EventSystem {
    public enum EventKind {
        ToggleVisibility,
        ImageUpdate,
        Animation,
        Victory,
        Defeat,
        Label,
    }

    public partial class EventHandler : Control {
        public static List<Event> Queue { get; set; } = new();

        private bool _processing;

        public static void ThrowEvent(Event ev) => Queue.Add(ev);

        public override void _Process(double delta) {
            if (_processing || Queue.Count == 0)
                return;

            _processing = true;
            _ = ProcessNextEventAsync();
        }

        private async Task ProcessNextEventAsync() {
            try {
                Event current = Queue[0];
                Queue.RemoveAt(0);

                foreach (Func<Task<int>> func in current.body.funcs) {
                    if (await func() != 0)
                        GD.PushError($"Event '{current.Name}' failed.");
                }
            }
            catch (Exception exception) {
                GD.PushError($"Event processing failed: {exception}");
            }
            finally {
                _processing = false;
            }
        }
    }

    public class Event {
        public string Name { get; set; }
        public EventKind Kind { get; set; }
        public EventBody body { get; set; }

        public Event(string name, EventKind kind, EventBody body) {
            Name = name;
            Kind = kind;
            this.body = body;
        }
    }

    public partial class EventBody : Node {
        public List<Func<Task<int>>> funcs { get; set; } = new();

        public static EventBody ImageUpdate(Panel panel, Image image, string character) {
            return new EventBody {
                funcs = [
                    () => Task.FromResult(changePanelImage(panel, image, character))
                ]
            };
        }

        public static EventBody Animation(Node node, string anim) {
            var body = new EventBody();

            if (node is AnimationPlayer animationPlayer)
                body.funcs = [() => body.animPlay(animationPlayer, anim)];
            else if (node is AnimatedSprite2D animatedSprite)
                body.funcs = [() => body.animPlay(animatedSprite, anim)];
            else {
                GD.PushError( "Animation: node must be an AnimationPlayer or AnimatedSprite2D.");
                body.funcs = [() => Task.FromResult(1)];
            }

            return body;
        }

        public static EventBody Animation(Node node, string anim, int delay) {
            var body = new EventBody();

            if (node is AnimationPlayer animationPlayer)
                body.funcs = [() => body.animPlay(animationPlayer, anim, delay)];
            else if (node is AnimatedSprite2D animatedSprite)
                body.funcs = [() => body.animPlay(animatedSprite, anim, delay)];
            else {
                GD.PushError( "Animation: node must be an AnimationPlayer or AnimatedSprite2D.");
                body.funcs = [() => Task.FromResult(1)];
            }

            return body;
        }


        public static EventBody Label(Label node, string content) {
            return new EventBody {
                funcs = [ () => Task.FromResult(changeLabelText(node, content)) ]
            };
        }

        public static EventBody ToggleVisibility(Node node) {
            var body = new EventBody();

            if (node is Control ctrl) {
                body.funcs = [ () => Task.FromResult(toggleVisibility(ctrl))];
            }
            else if (node is Node2D node2d) {
                body.funcs = [ () => Task.FromResult(toggleVisibility(node2d))];
            }
            else {
                GD.PushError("ToggleVisibility: node must be a Node2D or Cotrol");
                body.funcs = [ () => Task.FromResult(1)];
            }

            return body;
        }


        private async Task<int> animPlay(AnimationPlayer node, string anim, int delayMs) {
            if (node == null) { GD.PushError("animPlay: AnimationPlayer reference is null."); return 1; }
            if (delayMs < 0) { GD.PushError("animPlay: delay time cannot be negative."); return 1; }

            node.Play(anim);
            await Task.Delay(delayMs);
            return 0;
        }

        private async Task<int> animPlay(AnimatedSprite2D node, string anim, int delayMs) {
            if (node == null) { GD.PushError("animPlay: AnimatedSprite2D reference is null."); return 1; }
            if (delayMs < 0) { GD.PushError("animPlay: delay time cannot be negative."); return 1; }

            node.Play(anim);
            await Task.Delay(delayMs);
            return 0;
        }

        private async Task<int> animPlay(AnimationPlayer node, string anim) {
            if (node == null) { GD.PushError("animPlay: AnimationPlayer reference is null."); return 1; }

            node.Play(anim);
            await ToSignal(node, AnimationPlayer.SignalName.AnimationFinished);
            return 0;
        }

        private async Task<int> animPlay(AnimatedSprite2D node, string anim) {
            if (node == null) { GD.PushError("animPlay: AnimatedSprite2D reference is null."); return 1; }

            node.Play(anim);
            await ToSignal(node, AnimatedSprite2D.SignalName.AnimationFinished);
            return 0;
        }

        private static int toggleVisibility(Control node) {
            node.Visible = !node.Visible;
            return 0;
        }
        
        private static int toggleVisibility(Node2D node) {
            node.Visible = !node.Visible;
            return 0;
        }

        private static int changeLabelText(Label label, string content) {
            if (label == null) {
                GD.PushError("changeLabelText: Label reference is null.");
                return 1;
            }

            label.Text = content;
            return 0;
        }

        private static int changePanelImage(Panel panel, Image image, string character) {
            if (panel == null || image == null) {
                GD.PushError("changePanelImage: null reference.");
                return 1;
            }

            ImageTexture texture = ImageTexture.CreateFromImage(image);
            StyleBoxTexture style = new StyleBoxTexture { Texture = texture };
            panel.AddThemeStyleboxOverride("panel", style);
            return 0;
        }
    }
}


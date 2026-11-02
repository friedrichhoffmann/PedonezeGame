using Godot;

public partial class PlayerWalk : CharacterBody2D
{
    [Export]
    public float Speed = 300.0f;

    [Export]
    public float Deceleration = 1200.0f;

    private AnimatedSprite2D animSprite;

    public override void _Ready()
    {
        animSprite = FindChild("AnimatedSprite2D", recursive: true) as AnimatedSprite2D;

        if (animSprite == null)
            GD.PushError("AnimatedSprite2D wasn't found under the player.");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector(
            "ui_left", "ui_right", "ui_up", "ui_down"
        );

        Vector2 velocity = Velocity;
        string animation = "default";

        if (direction != Vector2.Zero)
        {
            velocity = direction * Speed;

            // Choose the animation for the dominant movement direction.
            if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
            {
                animation = "h_walk";
                animSprite.FlipH = direction.X > 0; // h_walk faces left
            }
            else if (direction.Y > 0)
            {
                animation = "down_walk";
                animSprite.FlipH = false;
            }
            else
            {
                animation = "up_walk";
                animSprite.FlipH = false;
            }
        }
        else
        {
            float amount = Deceleration * (float)delta;
            velocity.X = Mathf.MoveToward(velocity.X, 0, amount);
            velocity.Y = Mathf.MoveToward(velocity.Y, 0, amount);
        }

        Velocity = velocity;
        MoveAndSlide();

        // Don't restart the animation every physics frame.
        if (animSprite.Animation != animation)
            animSprite.Play(animation);
    }
}


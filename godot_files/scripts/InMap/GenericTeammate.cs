using Godot;
using System.Collections.Generic;
using System.Linq;
using System;
using ClockFile;
using MainCharacterFile;

namespace GenericTeammateFile;

public partial class GenericTeammate : CharacterBody2D {
    private bool torch;
    private string objectSenePrefix = "scenes/ObjectsAsScenes/Characters";


    [Export] public float Speed = 300.0f;
    [Export] public float Acceleration = 1500.0f; // how fast it reaches full speed
    [Export] public float Deceleration = 1800.0f; // how fast it stops (was 100, which caused the glide)
    
    public bool InBattle = false;
    public MainCharacter CharacterClass { get; set; } 

    public bool Torch {
        get => torch;
        set {
            if (value) {
#nullable enable
                GenericTeammate? current = GetTorchHolder();
#nullable disable
                if (current != null && current != this)
                    current.torch = false;
            }
            torch = value;
        }
    }

    public GenericTeammate[] GetTeam() {
        Node parent = GetParent();
        if (parent is null)
            return [];

        return [.. parent.GetChildren()
            .OfType<GenericTeammate>()];
    }

#nullable enable
    public GenericTeammate? GetTorchHolder() => GetTeam().FirstOrDefault(tm => tm.Torch);
#nullable disable

    public override void _Ready() {
        try {
            var shape = GetNodeOrNull<CollisionShape2D>("Collision");
            GD.Print(CharacterClass?.GetType().FullName);

            SetAnimatedSprite2D();
            if (!Torch) SetNavigation();
        }
        catch (Exception e) {
            GD.PrintErr($"{Name}: setup failed: {e}");
        }
    }

    public override void _PhysicsProcess(double delta) {
        if (Torch) Move(delta, PlayerMovementInput());
        else Move(delta, FollowTorch(delta));
    }

    public void Move(double delta, Vector2 direction) {
        Vector2 targetVelocity = direction * Speed;
        float rate = (direction != Vector2.Zero ? Acceleration : Deceleration) * (float)delta;

        Vector2 velocity = Velocity;
        velocity.X = Mathf.MoveToward(velocity.X, targetVelocity.X, rate);
        velocity.Y = Mathf.MoveToward(velocity.Y, targetVelocity.Y, rate);
        Velocity = velocity;

        MoveAndSlide();

        Animate(direction);
    }

    private void Stop(double delta) {
        float amount = Deceleration * (float)delta;
        var velocity = Velocity;
        velocity.X = Mathf.MoveToward(velocity.X, 0, amount);
        velocity.Y = Mathf.MoveToward(velocity.Y, 0, amount);
        Velocity = velocity;
    }
}

// Player Only Methods
public partial class GenericTeammate {
    private void SetCamera() {
        string cameraScenePath = $"res://scenes/ObjectsAsScenes/Others/InMapCamera.tscn";

        PackedScene packedCameraScene = ResourceLoader.Load<PackedScene>(cameraScenePath);

        Camera2D cameraNode = packedCameraScene.Instantiate<Camera2D>();
        AddChild(cameraNode);
    }

    public Vector2 PlayerMovementInput() {
        return Input.GetVector(
            "ui_left", "ui_right", "ui_up", "ui_down"
        );
    }
}

// Ally Only Methods
public partial class GenericTeammate {


    private const int FOLLOWING = 1;
    private const int WAITING = 0;

    private int state = WAITING;
    private Vector2 playerTail = Vector2.Down;

    [Export] public float FollowDistance = 105.0f;
    [Export] public float ResumeDistance = 18.0f;
    [Export] public float StopDistance = 4.0f;

#nullable enable
    private NavigationAgent2D GetNavigation() => GetNode<NavigationAgent2D>("Nav/Navigation");
#nullable disable

    private void SetNavigation() {
        if (GetNodeOrNull("Nav/Navigation") != null)
            return;

        string navigationScenePath = $"res://{objectSenePrefix}/AllySpecific/Navigation.tscn";

        Node2D navigationRoot = GetNode<Node2D>("Nav");
        PackedScene packedNavigationScene = ResourceLoader.Load<PackedScene>(navigationScenePath);

        NavigationAgent2D navigationNode = packedNavigationScene.Instantiate<NavigationAgent2D>();
        navigationNode.Name = "Navigation";
        navigationRoot.AddChild(navigationNode);
    }

    public Vector2 FollowTorch(double delta) {
#nullable enable
        GenericTeammate? holder = GetTorchHolder();
#nullable disable

        if (holder == null) {
            GD.Print($"{Name}: no torch holder found; parent={GetParent()?.Name}");
            return Vector2.Zero;
        }

        if (holder == null)
            return Vector2.Zero;

        UpdatePlayerTail();

        var followers = GetTeam()
            .Where(tm => tm != holder)
            .ToArray();

        int slot = Array.IndexOf(followers, this);
        Vector2 side = new Vector2(-playerTail.Y, playerTail.X);
        float spacing = 36.0f;
        float lateralOffset = (slot - (followers.Length - 1) / 2.0f) * spacing;

        Vector2 targetPosition =
            holder.GlobalPosition
            - playerTail * FollowDistance
            + side * lateralOffset;

        NavigationAgent2D nav = GetNavigation();

        if (nav.TargetPosition.DistanceTo(targetPosition) > 3.0f)
            nav.TargetPosition = targetPosition;

        float distance = GlobalPosition.DistanceTo(targetPosition);

        // Hysteresis: stop when close, only resume once the holder has pulled far enough away.
        if (state == FOLLOWING && distance <= StopDistance)
            state = WAITING;
        else if (state == WAITING && distance > ResumeDistance)
            state = FOLLOWING;

        if (state == WAITING)
            return Vector2.Zero;

        // Use the navmesh path if one exists, otherwise walk straight at the target.
        Vector2 nextPosition = nav.IsNavigationFinished() ? targetPosition : nav.GetNextPathPosition();
        return GlobalPosition.DirectionTo(nextPosition);
    }

    private void UpdatePlayerTail() {
#nullable enable
        GenericTeammate? holder = GetTorchHolder();
#nullable disable
        if (holder != null && holder.Velocity.Length() > 5.0f)
            playerTail = holder.Velocity.Normalized();
    }
}


// Animation Related Methods
public partial class GenericTeammate {

    private string GetAnimatedSpriteScenePath() {
        return CharacterClass switch {
            AllyFile.Archer    => $"res://{objectSenePrefix}/AnimatedSprites/Archer.tscn",
            AllyFile.Cleric    => $"res://{objectSenePrefix}/AnimatedSprites/Cleric.tscn",
            AllyFile.Gladiator => $"res://{objectSenePrefix}/AnimatedSprites/Gladiator.tscn",
            AllyFile.Wizard    => $"res://{objectSenePrefix}/AnimatedSprites/Wizard.tscn",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetAnimatedSprite2D() {
        var spr = GetNodeOrNull<Node2D>("Spr");
        if (spr == null) {
            GD.PrintErr($"{Name}: missing Spr; path={GetPath()}");
            return;
        }

        string path = GetAnimatedSpriteScenePath();
        GD.Print($"For {CharacterClass?.GetType().Name}: {path}");

        var packed = ResourceLoader.Load<PackedScene>(path);
        if (packed == null) {
            GD.PrintErr($"{Name}: could not load animation scene: {path}");
            return;
        }

        var animationNode = packed.Instantiate();
        animationNode.Name = "AnimatedSprite2D";
        spr.AddChild(animationNode);

        GD.Print($"{Name}: added {animationNode.GetPath()} ({animationNode.GetClass()})");
    }

    private string GetAnimation(Vector2 direction) {
        if (direction == Vector2.Zero) return "Idle";

        int[] AxisDirection = (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y)) ?
            [Mathf.Sign(direction.X),0] : [0,Mathf.Sign(direction.Y)];

        return AxisDirection switch {
            [1, 0] => "Right",
            [-1,0] => "Left",
            [0, 1] => "Front",
            [0, -1] => "Back",
            _ => "Idle"
        };
    }
    private void Animate(Vector2 direction) {
#nullable enable
        AnimatedSprite2D? anim = GetNodeOrNull<AnimatedSprite2D>("Spr/AnimatedSprite2D");
#nullable disable
        if (anim == null) {
            GD.PrintErr($"{Name}: no AnimatedSprite2D found under Spr");
            return;
        }
        if (anim.SpriteFrames == null) {
            GD.PrintErr($"{Name}: AnimatedSprite2D has no SpriteFrames");
            return;
        }

        string animation = GetAnimation(direction);

        anim.FlipH = animation == "Right";
        if (anim.FlipH) animation = "Left";

        if (!anim.SpriteFrames.HasAnimation(animation)) {
            GD.PrintErr($"{Name}: animation '{animation}' not in SpriteFrames. Available: {string.Join(", ", anim.SpriteFrames.GetAnimationNames())}");
            return;
        }

        if (anim.Animation != animation || !anim.IsPlaying())
            anim.Play(animation);
    }

}


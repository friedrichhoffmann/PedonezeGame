using Godot;
using System.Linq;
using GenericTeammateFile;

public partial class CameraMove : Camera2D
{
    private const int FOLLOWING = 1;
    private const int WAITING = 0;

    private int state = WAITING;

    [Export] public float FollowDistance = 0.0f;
    [Export] public float ResumeDistance = 20.0f;
    [Export] public float CameraSpeed = 300.0f;

    public override void _Ready()
    {
        var holder = GetTorchHolder();
        if (holder != null)
            GlobalPosition = holder.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (GetTorchHolder() == null)
            return;

        if (state == WAITING)
            WaitStill();
        else
            FollowTorch(delta);
    }

    private void FollowTorch(double delta)
    {
        if (DistanceToCurrentTarget() <= FollowDistance)
        {
            state = WAITING;
            return;
        }

        var target = GetTorchHolder()!.GlobalPosition;
        GlobalPosition = GlobalPosition.MoveToward(
            target,
            CameraSpeed * (float)delta
        );
    }

    private void WaitStill()
    {
        if (DistanceToCurrentTarget() > ResumeDistance)
            state = FOLLOWING;
    }

    private float DistanceToCurrentTarget()
    {
        var holder = GetTorchHolder();
        return holder == null
            ? 0f
            : GlobalPosition.DistanceTo(holder.GlobalPosition);
    }

    private GenericTeammate? GetTorchHolder()
    {
        return GetPlayerTeam()?.FirstOrDefault(tm => tm.Torch);
    }

    private GenericTeammate[]? GetPlayerTeam()
    {
        var playerTeam = GetTree().Root.FindChild("PlayerTeam", true, false);
        return playerTeam?.GetChildren()
            .OfType<GenericTeammate>()
            .ToArray();
    }
}


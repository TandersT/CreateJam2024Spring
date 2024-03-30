using System.Linq;
using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    int Speed = 250;
    [Export]
    int DashSpeed = 10000;
    [Export]
    float DashTaperSpeed = 0.66f;

    float activeDashSpeed = 0;
    public override void _Ready()
    {
        Global.Player = this;
    }

    public override void _Process(double delta)
    {
        Global.AllNpcs.Where(x => x.Position.DistanceSquaredTo(this.Position) >= Global.MaxDistanceToNpc).ToList().ForEach(x => x.RangeStatus = RangeStatusEnum.Outside);
        var npcsInRange = Global.AllNpcs.Where(x => x.Position.DistanceSquaredTo(this.Position) < Global.MaxDistanceToNpc).OrderBy(x => x.Position.DistanceSquaredTo(this.Position));
        var closest = npcsInRange.FirstOrDefault();
        var rest = npcsInRange.Where(x => x != closest).ToList();
        if (closest != null)
        {
            closest.RangeStatus = RangeStatusEnum.InsideFocus;
        }
        rest.ForEach(x => x.RangeStatus = RangeStatusEnum.InsideUnfocus);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 movementDirection = Vector2.Zero;

        if (Input.IsActionPressed("Up"))
            movementDirection.Y = -1;
        if (Input.IsActionPressed("Down"))
            movementDirection.Y = 1;
        if (Input.IsActionPressed("Left"))
            movementDirection.X = -1;
        if (Input.IsActionPressed("Right"))
            movementDirection.X = 1;

        movementDirection = movementDirection.Normalized();

        activeDashSpeed = activeDashSpeed * DashTaperSpeed;
        var movementSpeed = movementDirection * (Speed + activeDashSpeed) * (float)delta;

        // MoveAndCollide(movementSpeed);
        Velocity = movementSpeed * 50;
        MoveAndSlide();
        LookAt(GetGlobalMousePosition());
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("Dash"))
        {
            activeDashSpeed = DashSpeed;
        }
        if (@event.IsActionReleased("Interact"))
        {
            var _closestNpc = GetClosestNpc();
            if (_closestNpc != null)
            {
                _closestNpc.OnInteractedWith();
            }
        }
    }

    Npc GetClosestNpc()
    {
        return Global.AllNpcs.Where(x => x.RangeStatus == RangeStatusEnum.InsideFocus).FirstOrDefault();
    }
}

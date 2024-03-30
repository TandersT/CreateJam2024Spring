using System.Linq;
using Godot;

public partial class Player : CharacterBody2D, IResetable
{
    [Export]
    int Speed = 250;
    [Export]
    int DashSpeed = 10000;
    [Export]
    float DashTaperSpeed = 0.66f;

    float activeDashSpeed = 0;

    bool canDash = true;
    Npc closestNpc;
    Vector2 StartPosition;
    AnimationTree AnimationTree => GetNode<AnimationTree>("%AnimationTree");
    public override void _Ready()
    {
        StartPosition = GlobalPosition;
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

        UpdateClosestNpc();
        if (Global.GameState == GameStateEnum.KillingPhase ||
        Global.GameState == GameStateEnum.TalkingPhase)
        {
            if (closestNpc == null)
            {
                Global.DialogueUI.HideDialogue();
            }
        }
        if (Global.GameState == GameStateEnum.KillingPhase)
        {
            if (closestNpc != null && Global.DialogueUI.MenuOpen == false)
            {
                closestNpc.OnInteractedWith();
            }
        }

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
        var movementSpeed = movementDirection * (Speed + activeDashSpeed);


        AnimationTree.Set("parameters/movement/blend_position", movementDirection);
        AnimationTree.Set("parameters/add_sound/add_amount", movementDirection.Abs().Length() != 0 ? 1 : 0);
        // MoveAndCollide(movementSpeed);
        Velocity = movementSpeed;
        MoveAndSlide();
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Dash") && canDash)
        {
            activeDashSpeed = DashSpeed;
            canDash = false;
            CreateTween().TweenInterval(0.33f).Finished += () => canDash = true;
        }
        if (@event.IsActionPressed("Interact"))
        {
            if (Global.GameState == GameStateEnum.TalkingPhase)
            {

                if (closestNpc != null && Global.DialogueUI.MenuOpen == false ||
                    closestNpc != null && closestNpc != Global.DialogueUI.ActiveNpc)
                {
                    if (closestNpc != Global.DialogueUI.ActiveNpc && Global.DialogueUI.ActiveNpc != null)
                    {
                        Global.DialogueUI.ActiveNpc.InteractionStatus = InteractionStatusEnum.Interacted;
                    }
                    closestNpc.OnInteractedWith();
                }
            }
        }
    }

    void UpdateClosestNpc()
    {
        closestNpc = Global.AllNpcs.Where(x => x.RangeStatus == RangeStatusEnum.InsideFocus).FirstOrDefault();
    }

    public void Reset()
    {
        GlobalPosition = StartPosition;
    }

}

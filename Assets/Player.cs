using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Player : CharacterBody2D, IResetable
{
    int introSpeedIndex = 0;
    List<string> IntroSpeech = new()
    {
        "Now that I have assumed my human form I can walk undetected amongst the humans.",
        "I need to act fast through, I can only maintain this form a limited time.",
        "I wonder which of these duds are actually worth sacrifing.. I should try talk with them.",
        "I guess I can figure out their role and strength that way."
    };

    List<string> SecondSpeech = new()
    {
        "I should make sure I get a varied cast of skills",
        "A game with only programming tends to lack in other departments..",
        "I've seen my fair share of programmers in hell who refused to add proper graphics",
    };

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
    AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer>("%AnimationPlayer");
    AnimationTree AnimationTree => GetNode<AnimationTree>("%AnimationTree");
    AnimationTree AnimationTreeDemon => GetNode<AnimationTree>("%AnimationTreeDemon");
    AnimationPlayer Text => GetNode<AnimationPlayer>("%Text");
    Label SpeechLabel => GetNode<Label>("%SpeechLabel");
    AnimatedSprite2D Sprite => GetNode<AnimatedSprite2D>("%Sprite");

    public override void _EnterTree()
    {
        Global.OnGameStateChangedDelegate += (GameStateEnum gameState) =>
        {
            switch (gameState)
            {
                case GameStateEnum.TalkingPhase:
                    AnimationTree.Active = true;
                    AnimationTreeDemon.Active = false;
                    Sprite.Play("idle");
                    break;
                case GameStateEnum.KillingPhase:
                    AnimationTreeDemon.Active = true;
                    AnimationTree.Active = false;
                    Sprite.Play("demon_idle");
                    break;
            }
            if (gameState == GameStateEnum.TalkingPhase && Global.RoundCount == 0)
            {
                Text.Play("Text");
                AdvanceText(IntroSpeech);
            }
            else if (gameState == GameStateEnum.TalkingPhase && Global.RoundCount == 1)
            {
                Text.Play("Text");
                AdvanceText(SecondSpeech);
            }
            else
            {
                Text.Play("RESET");
                introSpeedIndex = 0;
            }
        };
    }

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
        var prevCurrent = closestNpc;
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
            if (closestNpc != null && Global.DialogueUI.MenuOpen == false ||
            prevCurrent != closestNpc && closestNpc != null && Global.DialogueUI.MenuOpen == false)
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
        AnimationTreeDemon.Set("parameters/movement/blend_position", movementDirection);
        AnimationTree.Set("parameters/add_sound/add_amount", movementDirection.Abs().Length() != 0 ? 1 : 0);
        // MoveAndCollide(movementSpeed);
        Velocity = movementSpeed;
        MoveAndSlide();


    }

    void AdvanceText(List<string> text)
    {
        if (introSpeedIndex < text.Count)
        {
            SpeechLabel.VisibleRatio = 0;
            SpeechLabel.Text = text[introSpeedIndex++];
            var tween = CreateTween();
            Global.tweens.Add(tween);
            tween.TweenProperty(SpeechLabel, "visible_ratio", 1, SpeechLabel.Text.Length / 30f);
            tween.TweenInterval(2);
            tween.Finished += () => AdvanceText(text);
        }
        else
        {
            Text.Play("RESET");
        }
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
        introSpeedIndex = 0;
        Text.Play("RESET");
    }

    public void Suck()
    {
        AnimationTreeDemon.Active = false;
        AnimationPlayer.Play("suck_demon");
    }

}

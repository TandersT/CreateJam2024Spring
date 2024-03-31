using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public enum ProficiencyEnum
{
    None = 0,
    Novice = 1,
    Intermediate = 2,
    Expert = 3,
}

public enum RoleEnum
{
    Peasent,
    Programmer,
    SoundDesigner,
    GraphicsArtist,
}

public enum RangeStatusEnum
{
    Outside,
    InsideUnfocus,
    InsideFocus,
}

public enum InteractionStatusEnum
{
    NoInteraction,
    Interacted,
}

public enum CommuncationTypeEnum
{
    Greeting,
    General,
    Role,
    Proficiency,
}

public partial class Npc : CharacterBody2D
{
    public RoleEnum Role;
    public ProficiencyEnum Proficiency;

    [Export]
    AudioStream[] ProgrammerAudio;
    [Export]
    AudioStream[] SoundAudio;
    [Export]
    AudioStream[] GraphicsAudio;

    public InteractionStatusEnum InteractionStatus = InteractionStatusEnum.NoInteraction;

    public List<CommuncationTypeEnum> FilledCommuncationTypes = new()
    {
        CommuncationTypeEnum.Greeting
    };
    public List<string> IgnoreLines = new();
    public List<string> Lines = new();
    public int lineIndex = 0;

    Label RoleLabel => GetNode<Label>("%RoleLabel");
    Label ProficiencyLabel => GetNode<Label>("%ProficiencyLabel");
    AnimatedSprite2D Icon => GetNode<AnimatedSprite2D>("%Icon");
    NavigationAgent2D Navigation => GetNode<NavigationAgent2D>("%NavigationAgent2D");
    AudioStreamPlayer2D AudioStreamPlayer2D => GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
    AudioStreamPlayer Suck => GetNode<AudioStreamPlayer>("%Suck");
    AudioStreamPlayer Scream => GetNode<AudioStreamPlayer>("%Scream");
    AnimatedSprite2D SoulSprite => GetNode<AnimatedSprite2D>("%SoulSprite");

    List<string> animations = new()
    {
        "1",
        "2",
        "3",
        "4",
        "5",
    };


    RangeStatusEnum rangeStatus = RangeStatusEnum.Outside;
    public RangeStatusEnum RangeStatus
    {
        get => rangeStatus;
        set
        {
            RangeStatusUpdated();
            rangeStatus = value;
        }
    }

    Color FocusColor = Colors.Green;
    Color UnfocusColor = Colors.Gray;
    ShaderMaterial OutlineMaterial;
    public override void _EnterTree()
    {
        Global.AllNpcs.Add(this);
        if (Role == RoleEnum.Peasent)
        {
            Proficiency = ProficiencyEnum.None;
            FilledCommuncationTypes.Add(CommuncationTypeEnum.Role);
            FilledCommuncationTypes.Add(CommuncationTypeEnum.Proficiency);
        }

        RoleLabel.Text = Role.ToString();
        ProficiencyLabel.Text = Proficiency.ToString();

        Icon.Animation = animations[Helper.RandomInt(0, animations.Count)];
        Icon.SpeedScale = Helper.RandomFloat(0.9f, 1.1f);
        Icon.Frame = Helper.RandomInt(0, 2);

        OutlineMaterial = (ShaderMaterial)Icon.Material.Duplicate();
        Icon.Material = OutlineMaterial;

        Icon.Play();

        SetupLines();

        Navigation.TargetPosition = Vector2.One * 100;
        // Navigation.VelocityComputed += UpdateVelocity;




    }

    public override void _Ready()
    {
        if (Helper.RandomPolarity() == 1)
        {
            switch (Role)
            {
                case RoleEnum.Programmer:
                    AudioStreamPlayer2D.Stream = ProgrammerAudio[Helper.RandomInt(0, ProgrammerAudio.Length)];
                    break;
                case RoleEnum.SoundDesigner:
                    AudioStreamPlayer2D.Stream = SoundAudio[Helper.RandomInt(0, SoundAudio.Length)];
                    break;
                case RoleEnum.GraphicsArtist:
                    AudioStreamPlayer2D.Stream = GraphicsAudio[Helper.RandomInt(0, GraphicsAudio.Length)];
                    break;

            }
            AudioStreamPlayer2D.Play();
        }
    }

    private void UpdateVelocity(Vector2 safeVelocity)
    {
        Velocity = safeVelocity;
        MoveAndSlide();
    }


    public void SetupLines()
    {
        switch (Role)
        {
            case RoleEnum.Peasent:
                GetLines(Global.PeasentRole);
                break;
            case RoleEnum.Programmer:
                GetLines(Global.ProgrammerRole);
                break;
            case RoleEnum.SoundDesigner:
                GetLines(Global.SoundDesignerRole);
                break;
            case RoleEnum.GraphicsArtist:
                GetLines(Global.GraphicsArtistRole);
                break;
        }
    }

    public void GetLines(IProficienct iProfcient)
    {

        var greetingLines = new List<string>(iProfcient.Greetings);
        var generalLines = new List<string>(iProfcient.General);
        var proficiencyLines = new List<string>();
        var roleLines = new List<string>(iProfcient.Role);
        switch (Proficiency)
        {
            case ProficiencyEnum.Novice:
                proficiencyLines = new List<string>(iProfcient.Novice);
                break;
            case ProficiencyEnum.Intermediate:
                proficiencyLines = new List<string>(iProfcient.Intermediate);
                break;
            case ProficiencyEnum.Expert:
                proficiencyLines = new List<string>(iProfcient.Expert);
                break;
        }

        for (int i = 0; i < 10; i++)
        {
            var list = SetupDialog(i, generalLines, greetingLines, proficiencyLines, roleLines);
            var index = Helper.RandomInt(0, list.Count);

            Lines.Add(list[index]);

            list.RemoveAt(index);
        }

        List<string> availableIgnoreLines = new List<string>(iProfcient.HasInteracted);
        for (int i = 0; i < 3; i++)
        {
            var index = Helper.RandomInt(0, availableIgnoreLines.Count);
            string text = availableIgnoreLines[index];
            IgnoreLines.Add(text);
            availableIgnoreLines.RemoveAt(index);
        }
    }

    public void OnInteractedWith()
    {
        Global.DialogueUI.PrepareNpcDialogue(this);
    }

    public override void _PhysicsProcess(double delta)
    {
        // var movement_delta = 1000;

        // var next_path_position = Navigation.GetNextPathPosition();

        // var new_velocity = GlobalPosition.DirectionTo(next_path_position) * movement_delta;
        // Navigation.Velocity = new_velocity;

    }
    public List<string> SetupDialog(int index, List<string> general, List<string> greeting, List<string> proficiency, List<string> role)
    {
        CommuncationTypeEnum chosenType = Helper.GetRandomEnumValue<CommuncationTypeEnum>(FilledCommuncationTypes.ToArray());
        if (index == 0)
        {
            chosenType = Helper.RandomFloat(0, 1f) > 0.3f ? CommuncationTypeEnum.Greeting : chosenType;
        }
        switch (chosenType)
        {
            case CommuncationTypeEnum.Greeting:
                FilledCommuncationTypes.Add(CommuncationTypeEnum.Greeting);
                return greeting;
            case CommuncationTypeEnum.General:
                return general;
            case CommuncationTypeEnum.Role:
                FilledCommuncationTypes.Add(CommuncationTypeEnum.Role);
                return role;
            case CommuncationTypeEnum.Proficiency:
                FilledCommuncationTypes.Add(CommuncationTypeEnum.Proficiency);
                return proficiency;
        }
        return general;
    }

    public void RangeStatusUpdated()
    {
        switch (RangeStatus)
        {
            case RangeStatusEnum.Outside:
                OutlineMaterial.SetShaderParameter("active", false);
                break;
            case RangeStatusEnum.InsideUnfocus:
                OutlineMaterial.SetShaderParameter("active", true);
                OutlineMaterial.SetShaderParameter("line_color", UnfocusColor);
                break;
            case RangeStatusEnum.InsideFocus:
                OutlineMaterial.SetShaderParameter("active", true);
                OutlineMaterial.SetShaderParameter("line_color", FocusColor);
                break;

        }
    }

    internal void OnKilled()
    {
        Global.SkillContainer.UpdateValue(Role, (int)Proficiency);
        Suck.Play();
        Scream.Play();
        SoulSprite.Play();
    }
}

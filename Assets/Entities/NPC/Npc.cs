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

public partial class Npc : CharacterBody2D
{
    [Export]
    Texture2D[] NpcIcons;
    RoleEnum Role;
    ProficiencyEnum Proficiency;

    public InteractionStatusEnum InteractionStatus = InteractionStatusEnum.NoInteraction;
    // string NPCName;
    public List<string> IgnoreLines = new();
    public List<string> Lines = new();
    public int lineIndex = 0;

    Label RoleLabel => GetNode<Label>("%RoleLabel");
    Label ProficiencyLabel => GetNode<Label>("%ProficiencyLabel");
    Sprite2D Icon => GetNode<Sprite2D>("%Icon");
    NavigationAgent2D Navigation => GetNode<NavigationAgent2D>("%NavigationAgent2D");



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
        Role = Helper.GetRandomEnumValue<RoleEnum>();
        if (Role == RoleEnum.Peasent)
        {
            Proficiency = ProficiencyEnum.None;
        }
        else
        {
            Proficiency = Helper.GetRandomEnumValue<ProficiencyEnum>(ProficiencyEnum.None);
        }

        RoleLabel.Text = Role.ToString();
        ProficiencyLabel.Text = Proficiency.ToString();

        var icon = Helper.RandomInt(0, NpcIcons.Length);
        Icon.Texture = NpcIcons[icon];

        OutlineMaterial = (ShaderMaterial)Icon.Material.Duplicate();
        Icon.Material = OutlineMaterial;

        SetupLines();

        Navigation.TargetPosition = Vector2.One * 100;
        // Navigation.VelocityComputed += UpdateVelocity;
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

        var availableLines = new List<string>();
        if (Proficiency == ProficiencyEnum.None)
        {
            availableLines =  new List<string>(iProfcient.Novice);
        }
        else
        {
            switch (Proficiency)
            {
                case ProficiencyEnum.Novice:
                    {
                        availableLines =  new List<string>(iProfcient.Novice);
                        break;
                    }
                case ProficiencyEnum.Intermediate:
                    {
                        availableLines =  new List<string>(iProfcient.Intermediate);
                        break;
                    }
                case ProficiencyEnum.Expert:
                    {
                        availableLines =  new List<string>(iProfcient.Expert);
                        break;
                    }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                var index = Helper.RandomInt(0, iProfcient.Greetings.Count);
                var text = iProfcient.Greetings[index];
                Lines.Add(text);
                continue;
            }


            {
                var index = Helper.RandomInt(0, availableLines.Count);

                Lines.Add(availableLines[index]);

                availableLines.RemoveAt(index);
            }
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
    // public void DialogDecider()
    // {
    //     //Decides what type of info is given
    //     int randomNumber = GD.RandRange(0, 100);
    //     if (FirstInteract == true)
    //     {
    //         if (randomNumber < 70)
    //         {
    //             //Normal Greeting
    //         }
    //     }
    //     else
    //     {
    //         if (randomNumber < 25)
    //         {
    //             //Give general info
    //         }
    //         else if (randomNumber >= 25 && randomNumber < 50)
    //         {
    //             //Give role Info
    //         }
    //         else if (randomNumber >= 50 && randomNumber < 75)
    //         {
    //             //Give proficiency Info
    //         }
    //         else
    //         {
    //             //Give Info on project
    //         }
    //     }
    // }

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
    }
}

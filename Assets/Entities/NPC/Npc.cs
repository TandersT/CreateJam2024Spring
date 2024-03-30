using Godot;
using Godot.NativeInterop;
using System;
using System.Threading;

public enum ProficiencyEnum
{
    None,
    Novice,
    Intermediate,
    Expert
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

public partial class Npc : CharacterBody2D
{
    [Export]
    Texture2D[] NpcIcons;
    RoleEnum Role;
    ProficiencyEnum Proficiency;
    string NPCName;
    bool FirstInteract;


    Label RoleLabel => GetNode<Label>("%RoleLabel");
    Label ProficiencyLabel => GetNode<Label>("%ProficiencyLabel");

    Sprite2D Icon => GetNode<Sprite2D>("%Icon");



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
        Proficiency = Helper.GetRandomEnumValue<ProficiencyEnum>();

        RoleLabel.Text = Role.ToString();
        ProficiencyLabel.Text = Proficiency.ToString();

        var icon = Helper.RandomInt(0, NpcIcons.Length);
        Icon.Texture = NpcIcons[icon];

        OutlineMaterial = (ShaderMaterial)Icon.Material.Duplicate();
        Icon.Material = OutlineMaterial;
    }
    public void OnInteractedWith()
    {
        GD.Print(Position.DistanceSquaredTo(Global.Player.Position));
        var dialogUI = GetNode<DialogUI>("/root/Main/UI/DialogUI");
        dialogUI.Visible = true;
        var dialogText = dialogUI.GetNode<Label>("%DialogLabel");

        if (Role == RoleEnum.Programmer)
        {
            var programmerLists = GetNode<ProgrammerRole>("/root/Main/Roles/Programmer");
            int randomIndex;
            switch (Proficiency)
            {
                case ProficiencyEnum.Novice:
                    randomIndex = GD.RandRange(0, programmerLists.Novice.Count-1);
                    dialogText.Text = programmerLists.Novice[randomIndex];
                    break;
                case ProficiencyEnum.Intermediate:
                    randomIndex = GD.RandRange(0, programmerLists.Intermediate.Count-1);
                    dialogText.Text = programmerLists.Intermediate[randomIndex];
                    break;
                case ProficiencyEnum.Expert:
                    randomIndex = GD.RandRange(0, programmerLists.Expert.Count-1);
                    dialogText.Text = programmerLists.Expert[randomIndex];
                    break;
            }
        }

        if (Role == RoleEnum.GraphicsArtist)
        {
            var ArtistLists = GetNode<GraphicsArtist>("/root/Main/Roles/GraphicsArtist");
            int randomIndex;
            switch (Proficiency)
            {
                case ProficiencyEnum.Novice:
                    randomIndex = GD.RandRange(0, ArtistLists.Novice.Count-1);
                    dialogText.Text = ArtistLists.Novice[randomIndex];
                    break;
                case ProficiencyEnum.Intermediate:
                    randomIndex = GD.RandRange(0, ArtistLists.Intermediate.Count-1);
                    dialogText.Text = ArtistLists.Intermediate[randomIndex];
                    break;
                case ProficiencyEnum.Expert:
                    randomIndex = GD.RandRange(0, ArtistLists.Expert.Count-1);
                    dialogText.Text = ArtistLists.Expert[randomIndex];
                    break;
            }
        }

        if (Role == RoleEnum.SoundDesigner)
        {
            var SoundLists = GetNode<SoundDesignerRole>("/root/Main/Roles/SoundDesigner");
            int randomIndex;
            switch (Proficiency)
            {
                case ProficiencyEnum.Novice:
                    randomIndex = GD.RandRange(0, SoundLists.Novice.Count-1);
                    dialogText.Text = SoundLists.Novice[randomIndex];
                    break;
                case ProficiencyEnum.Intermediate:
                    randomIndex = GD.RandRange(0, SoundLists.Intermediate.Count-1);
                    dialogText.Text = SoundLists.Intermediate[randomIndex];
                    break;
                case ProficiencyEnum.Expert:
                    randomIndex = GD.RandRange(0, SoundLists.Expert.Count-1);
                    dialogText.Text = SoundLists.Expert[randomIndex];
                    break;
            }
        }

        if (Role == RoleEnum.Peasent)
        {
            //Change the things to general stuff.
            var SoundLists = GetNode<SoundDesignerRole>("/root/Main/Roles/SoundDesigner");
            int randomIndex;
            randomIndex = GD.RandRange(0, SoundLists.Novice.Count-1);
            dialogText.Text = SoundLists.Novice[randomIndex];
        }
    }

    public void DialogDecider()
    {
        //Decides what type of info is given
        int randomNumber = GD.RandRange(0, 100);
        if (FirstInteract == true)
        {
            if (randomNumber < 70)
            {
                //Normal Greeting
            }
        }
        else
        {
            if (randomNumber < 25)
            {
                //Give general info
            }
            else if (randomNumber >= 25 && randomNumber < 50)
            {
                //Give role Info
            }
            else if (randomNumber >= 50 && randomNumber < 75)
            {
                //Give proficiency Info
            }
            else
            {
                //Give Info on project
            }
        }
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
}

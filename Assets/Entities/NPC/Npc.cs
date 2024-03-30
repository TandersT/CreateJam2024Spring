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

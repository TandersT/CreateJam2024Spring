using Godot;
using System;

public partial class SkillContainer : HBoxContainer
{
	SkillSlider ProgrammerSlider => GetNode<SkillSlider>("%ProgrammerSlider");
	SkillSlider AudioSlider => GetNode<SkillSlider>("%AudioSlider");
	SkillSlider GraphicsSlider => GetNode<SkillSlider>("%GraphicsSlider");
	public override void _EnterTree()
	{
		Global.SkillContainer = this;
	}

	public void UpdateValue(RoleEnum role, int value)
	{
		switch (role)
		{
			case RoleEnum.Programmer:
				ProgrammerSlider.UpdateValue(value);
				break;
			case RoleEnum.SoundDesigner:
				AudioSlider.UpdateValue(value);
				break;
			case RoleEnum.GraphicsArtist:
				GraphicsSlider.UpdateValue(value);
				break;
		}
	}
}

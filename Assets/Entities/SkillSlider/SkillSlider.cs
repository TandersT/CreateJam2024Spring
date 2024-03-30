using Godot;
using System;

public partial class SkillSlider : VBoxContainer
{
	[Export]
	RoleEnum Role;

	[Export]
	Texture2D programmerTexture;
	[Export]
	Texture2D audioTexture;
	[Export]
	Texture2D graphicsTexture;

	VSlider Slider => GetNode<VSlider>("%Slider");
	TextureRect Image => GetNode<TextureRect>("%Image");

	public override void _Ready()
	{
        switch (Role)
        {
            case RoleEnum.Programmer:
				Image.Texture = programmerTexture;
                break;
            case RoleEnum.SoundDesigner:
				Image.Texture = audioTexture;
                break;
            case RoleEnum.GraphicsArtist:
				Image.Texture = graphicsTexture;
                break;
        }
    }

    public void UpdateValue(float change)
	{
		Slider.Value += change;
	}
}

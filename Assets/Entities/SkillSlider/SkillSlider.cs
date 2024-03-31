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
		Global.OnGameStateChangedDelegate += (GameStateEnum gameState) =>
		{
			if (gameState == GameStateEnum.Idle)
			{
				Slider.Value = 0;
			}
		};
	}

	public void UpdateValue(float change)
	{
		Slider.Value += change;
		Global.Score += change;
		switch (Role)
		{
			case RoleEnum.Programmer:
				Global.ProgrammerScore = (int)Slider.Value;
				break;
			case RoleEnum.SoundDesigner:
				Global.SoundDesignerScore = (int)Slider.Value;
				break;
			case RoleEnum.GraphicsArtist:
				Global.GraphicsArtistScore = (int)Slider.Value;
				break;
		}
	}
}

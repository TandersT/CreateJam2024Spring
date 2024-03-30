using Godot;
using System;

public partial class MenuUI : Control
{
	Button StartButton => GetNode<Button>("%StartButton");
	Button ControlsButton => GetNode<Button>("%ControlsButton");
	Button QuitButton => GetNode<Button>("%QuitButton");
	public override void _Ready()
	{
		StartButton.Pressed += () =>
		{
			Global.GameState = GameStateEnum.Active;
			Global.MenuState = MenuStateEnum.Ingame;
		};
		
		QuitButton.Pressed += () => GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
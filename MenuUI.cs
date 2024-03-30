using Godot;
using System;

public partial class MenuUI : Control
{
	Button StartButton => GetNode<Button>("%StartButton");
	Button ControlsButton => GetNode<Button>("%ControlsButton");
	Button QuitButton => GetNode<Button>("%QuitButton");
	Button TryAgain => GetNode<Button>("%TryAgain");

	VBoxContainer Menu => GetNode<VBoxContainer>("%Menu");
	VBoxContainer End => GetNode<VBoxContainer>("%End");
	public override void _Ready()
	{
		StartButton.GrabFocus();

		StartButton.Pressed += () =>
		{
			Global.GameState = GameStateEnum.IntroCutscene;
			Global.MenuState = MenuStateEnum.Ingame;
		};

		QuitButton.Pressed += () => GetTree().Quit();

		TryAgain.Pressed += () => Global.GameState = GameStateEnum.Idle;
		Global.OnGameStateChangedDelegate += OnGamestateChanged;
	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		Menu.Visible = false;
		End.Visible = false;
		switch (gameState)
		{
			case GameStateEnum.End:
				End.Visible = true;
				break;
			case GameStateEnum.Idle:
				Menu.Visible = true;
				break;
		}
	}
}

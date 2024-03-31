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
	VBoxContainer Pause => GetNode<VBoxContainer>("%Pause");

	Button ContinueButton => GetNode<Button>("%ContinueButton");
	Button ReturnButton => GetNode<Button>("%ReturnButton");
	Sprite2D Controls => GetNode<Sprite2D>("%Controls");
	GameStateEnum savedState;
	public override void _Ready()
	{

		ControlsButton.Pressed += Controls.Show;

		StartButton.Pressed += () =>
		{
			Global.GameState = GameStateEnum.IntroCutscene;
			Global.MenuState = MenuStateEnum.Ingame;
		};

		QuitButton.Pressed += () => GetTree().Quit();

		ContinueButton.Pressed += () =>
		{
			Global.MenuState = MenuStateEnum.Ingame;
			Global.GameState = savedState;
		};
		ReturnButton.Pressed += () => Global.GameState = GameStateEnum.Idle;

		TryAgain.Pressed += () => Global.GameState = GameStateEnum.Idle;
		Global.OnGameStateChangedDelegate += OnGamestateChanged;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.IsActionPressed("Pause"))
		{
			if (Global.GameState == GameStateEnum.TalkingPhase || Global.GameState == GameStateEnum.KillingPhase)
			{
				savedState = Global.GameState;
				Global.GameState = GameStateEnum.Paused;
				Global.MenuState = MenuStateEnum.Pause;
			}
			else if (Global.GameState == GameStateEnum.Paused)
			{
				Global.GameState = savedState;
				Global.MenuState = MenuStateEnum.Ingame;
			}
		}
	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		Menu.Visible = false;
		End.Visible = false;
		Pause.Visible = false;

		switch (gameState)
		{
			case GameStateEnum.End:
				End.Visible = true;
				TryAgain.CallDeferred("grab_focus");
				break;
			case GameStateEnum.Idle:
				Menu.Visible = true;
				StartButton.CallDeferred("grab_focus");
				break;
			case GameStateEnum.Paused:
				Pause.Visible = true;
				ContinueButton.CallDeferred("grab_focus");
				break;
		}
	}
}

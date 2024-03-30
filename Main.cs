using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class Main : Node2D
{
	[Export]
	public bool Quickstart = false;

	List<IResetable> Resetables;
	public override void _EnterTree()
	{

		Global.OnGameStateChangedDelegate += OnGamestateChanged;
	}
	public override void _Ready()
	{
		Resetables = new List<IResetable>
		{
			Global.DialogueUI,
			Global.GameUI,
			Global.Player,
		};
		Resetables.ForEach(x => x.Reset());


		if (Quickstart)
		{
			Global.GameState = GameStateEnum.TalkingPhase;
			Global.MenuState = MenuStateEnum.Ingame;
		}
		else
		{
			Global.GameState = GameStateEnum.Idle;
			Global.MenuState = MenuStateEnum.Menu;
		}
	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		switch (gameState)
		{
			case GameStateEnum.NaN:
			case GameStateEnum.Idle:
			case GameStateEnum.End:
				GetTree().Paused = true;
				break;
			case GameStateEnum.TalkingPhase:
			case GameStateEnum.KillingPhase:
				GetTree().Paused = false;
				break;
		}
		switch (gameState)
		{
			case GameStateEnum.RoundFinished:
				Resetables.ForEach(x => x.Reset());
				break;
			case GameStateEnum.Idle:
				Global.RoundCount = 0;
				break;
		}
	}
}

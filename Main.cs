using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class Main : Node2D
{
	[Export]
	public bool Quickstart = false;

	AudioStreamPlayer Menu => GetNode<AudioStreamPlayer>("%Menu");
	AudioStreamPlayer Game => GetNode<AudioStreamPlayer>("%Game");

	public List<IResetable> Resetables;
	public override void _EnterTree()
	{
		Global.Main = this;
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
			Global.GameState = GameStateEnum.RoundFinished;
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
			case GameStateEnum.Paused:
				GetTree().Paused = true;
				Game.Stop();
				if (!Menu.Playing)
				{
					Menu.Play();
				}
				break;
			case GameStateEnum.TalkingPhase:
			case GameStateEnum.KillingPhase:
				GetTree().Paused = false;
				Menu.Stop();
				if (!Game.Playing)
				{
					Game.Play();
				}
				break;
		}
		switch (gameState)
		{
			case GameStateEnum.RoundFinished:
				break;
			case GameStateEnum.Idle:
				Global.RoundCount = 0;
				Global.Score = 0;
				Global.RoundDuration = 50;
				Global.ProgrammerScore = 0;
				Global.SoundDesignerScore = 0;
				Global.GraphicsArtistScore = 0;
				break;
		}
	}


}

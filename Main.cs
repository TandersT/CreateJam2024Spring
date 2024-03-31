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
	AudioStreamPlayer GameDevil => GetNode<AudioStreamPlayer>("%GameDevil");
	AudioStreamPlayer GameTalk => GetNode<AudioStreamPlayer>("%GameTalk");

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
				GameTalk.Stop();
				GameDevil.Stop();
				if (!Menu.Playing)
				{
					Menu.Play();
				}
				break;
			case GameStateEnum.TalkingPhase:
				GetTree().Paused = false;
				Menu.Stop();
				GameDevil.Stop();
				if (!GameTalk.Playing)
				{
					GameTalk.Play();
				}
				break;
			case GameStateEnum.KillingPhase:
				GetTree().Paused = false;
				Menu.Stop();
				GameTalk.Stop();
				if (!GameDevil.Playing)
				{
					GameDevil.Play();
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
				foreach (var item in Global.tweens)
				{
					if (item != null && item.IsValid())
					{
						item.Kill();
					}
				}
				break;

		}
	}


}

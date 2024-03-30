using Godot;
using System;
using System.Collections.Generic;

public partial class GameUI : Control, IResetable
{
	PanelContainer TalkingPhase => GetNode<PanelContainer>("%TalkingPhase");
	PanelContainer KillingPhase => GetNode<PanelContainer>("%KillingPhase");
	HSlider TimerSlider => GetNode<HSlider>("%TimerSlider");

	public PackedScene PackedNpc = GD.Load<PackedScene>("uid://c3in726u7mqpx");
	public override void _EnterTree()
	{
		Global.OnGameStateChangedDelegate += OnGamestateChanged;
		Global.GameUI = this;
	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		TalkingPhase.Hide();
		KillingPhase.Hide();
		if (gameState == GameStateEnum.TalkingPhase)
		{
			Prepare();
			TalkingPhase.Show();
		}
		if (gameState == GameStateEnum.KillingPhase)
		{
			KillingPhase.Show();
		}
	}

	public void Reset()
	{
		Global.AllNpcs.ForEach(x => x.QueueFree());
		Global.AllNpcs.Clear();
	}

	public void Prepare()
	{
		SpawnNpcs();

		TimerSlider.MaxValue = Global.RoundDuration;
		TimerSlider.Value = Global.RoundDuration;
		var tween = CreateTween();
		tween.TweenProperty(TimerSlider, "value", 0f, Global.RoundDuration);
		tween.Finished += () => Global.GameState = GameStateEnum.KillingPhase;

	}

	void SpawnNpcs()
	{
		int amount = Helper.RandomInt(5, 8);
		var availablePositions = new List<Vector2>(Global.SpawnablePositions);
		for (int i = 0; i < amount; i++)
		{
			var _npc = PackedNpc.Instantiate<Npc>();
			Global.NpcContatainer.AddChild(_npc);
			var index = Helper.RandomInt(0, availablePositions.Count);
			_npc.Position = availablePositions[index];
			availablePositions.RemoveAt(index);
		}
	}

}

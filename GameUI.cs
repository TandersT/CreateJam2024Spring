using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameUI : Control, IResetable
{

	PanelContainer TalkingPhase => GetNode<PanelContainer>("%TalkingPhase");
	PanelContainer KillingPhase => GetNode<PanelContainer>("%KillingPhase");
	HSlider TimerSlider => GetNode<HSlider>("%TimerSlider");

	PanelContainer DaysLeft => GetNode<PanelContainer>("%DaysLeft");
	Label DaysLeftLabel => GetNode<Label>("%DaysLeftLabel");
	AudioStreamPlayer DaysLeftAudio => GetNode<AudioStreamPlayer>("%DaysLeftAudio");

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
		if (gameState == GameStateEnum.RoundFinished)
		{
			ShowTimeLeft();
			TalkingPhase.Show();
		}
	}

	void ShowTimeLeft()
	{
		DaysLeft.Visible = true;
		var tween = CreateTween();
		DaysLeftAudio.Play();
		DaysLeft.Modulate = DaysLeft.Modulate with { A = 0 };
		DaysLeftLabel.Text = Global.DaysLeft[Global.RoundCount];
		tween.TweenProperty(DaysLeft, "modulate:a", 1, 1);
		tween.TweenCallback(Callable.From(() =>
		{
			Global.Main.Resetables.ForEach(x => x.Reset());
			SpawnNpcs();
		}
		));

		tween.TweenInterval(3);
		tween.TweenProperty(DaysLeft, "modulate:a", 0, 1);
		tween.Finished += () =>
		{
			DaysLeft.Visible = false;
			Global.GameState = GameStateEnum.RoundStarted; //TODO Animatin
			Global.GameState = GameStateEnum.TalkingPhase;
		};
	}

	public void Reset()
	{
		Global.AllNpcs.ForEach(x => x.QueueFree());
		Global.AllNpcs.Clear();
	}

	public void Prepare()
	{
		TimerSlider.MaxValue = Global.RoundDuration;
		TimerSlider.Value = Global.RoundDuration;
		var tween = CreateTween();
		tween.TweenProperty(TimerSlider, "value", 0f, Global.RoundDuration);
		tween.Finished += () => Global.GameState = GameStateEnum.KillingPhase;

	}

	void SpawnNpcs()
	{
		var availablePositions = new List<Vector2>(Global.SpawnablePositions);
		for (int i = 0; i < Helper.RandomInt(2, 6); i++)
		{
			AddNpc(ref availablePositions, RoleEnum.Peasent, ProficiencyEnum.None);
		}
		AddNpc(ref availablePositions, RoleEnum.Programmer, ProficiencyEnum.Novice, 0.85f);
		AddNpc(ref availablePositions, RoleEnum.GraphicsArtist, ProficiencyEnum.Novice, 0.85f);
		AddNpc(ref availablePositions, RoleEnum.SoundDesigner, ProficiencyEnum.Novice, 0.85f);

		AddNpc(ref availablePositions, RoleEnum.Programmer, ProficiencyEnum.Novice, 0.85f);
		AddNpc(ref availablePositions, RoleEnum.GraphicsArtist, ProficiencyEnum.Novice, 0.85f);
		AddNpc(ref availablePositions, RoleEnum.SoundDesigner, ProficiencyEnum.Novice, 0.85f);

		AddNpc(ref availablePositions, RoleEnum.Programmer, ProficiencyEnum.Novice, 0.85f);
		AddNpc(ref availablePositions, RoleEnum.GraphicsArtist, ProficiencyEnum.Novice, 0.85f);
		AddNpc(ref availablePositions, RoleEnum.SoundDesigner, ProficiencyEnum.Novice, 0.85f);

		AddNpc(ref availablePositions, RoleEnum.Programmer, ProficiencyEnum.Intermediate, 0.70f);
		AddNpc(ref availablePositions, RoleEnum.GraphicsArtist, ProficiencyEnum.Intermediate, 0.70f);
		AddNpc(ref availablePositions, RoleEnum.SoundDesigner, ProficiencyEnum.Intermediate, 0.70f);

		List<(RoleEnum, int)> scores = new List<(RoleEnum, int)>()
		{
			(RoleEnum.Programmer, Global.ProgrammerScore),
			(RoleEnum.SoundDesigner, Global.SoundDesignerScore),
			(RoleEnum.GraphicsArtist, Global.GraphicsArtistScore),
		};

		scores = scores.OrderBy(x => x.Item2).ToList();
		
		AddNpc(ref availablePositions, scores.First().Item1, ProficiencyEnum.Expert);
	}

	void AddNpc(ref List<Vector2> availablePositions, RoleEnum role, ProficiencyEnum proficiency, float chance = 1)
	{
		if (Helper.RandomFloat(0, 1) < chance)
		{
			var _npc = PackedNpc.Instantiate<Npc>();
			_npc.Role = role;
			_npc.Proficiency = proficiency;
			Global.NpcContatainer.AddChild(_npc);
			var index = Helper.RandomInt(0, availablePositions.Count);
			_npc.Position = availablePositions[index];
			availablePositions.RemoveAt(index);
		}
	}

}

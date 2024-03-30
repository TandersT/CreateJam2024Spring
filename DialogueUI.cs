using System;
using Godot;

public partial class DialogueUI : Control, IResetable
{
	VBoxContainer DialogueContainer => GetNode<VBoxContainer>("%DialogueContainer");
	Label DialougeLabel => GetNode<Label>("%DialougeLabel");
	RichTextLabel KillLabel => GetNode<RichTextLabel>("%KillLabel");
	Button KillButton => GetNode<Button>("%KillButton");
	RichTextLabel CutsceneLabel => GetNode<RichTextLabel>("%CutsceneLabel");

	HBoxContainer PryContainer => GetNode<HBoxContainer>("%PryContainer");
	HBoxContainer KillContainer => GetNode<HBoxContainer>("%KillContainer");
	HBoxContainer CutsceneContainer => GetNode<HBoxContainer>("%CutsceneContainer");

	Button Pry => GetNode<Button>("%Pry");
	Button DontPry => GetNode<Button>("%DontPry");


	float duration = 0.33f;

	public Npc ActiveNpc;
	public bool MenuOpen = false;

	bool dialogieInProcess;


	public override void _EnterTree()
	{
		Global.DialogueUI = this;
		Pry.Pressed += () =>
		{
			NpcDialogue();
		};
		DontPry.Pressed += () =>
		{
			ActiveNpc.InteractionStatus = InteractionStatusEnum.Interacted;
			Reset();
		};

		KillButton.Pressed += () =>
		{
			ActiveNpc.OnKilled();
			Global.GameState = GameStateEnum.KillingSelected;
			if (Global.RoundCount < Global.MaxRoundCount)
			{
				 Global.GameState = GameStateEnum.RoundFinished;
				Global.RoundCount++;
			}
			else
			{
				CreateTween().TweenInterval(1).Finished += () => Global.GameState = GameStateEnum.RoundFinished;
				if (true)//TODO ADD OPTION TO LOSE
				{
					CreateTween().TweenInterval(2).Finished += () => Global.GameState = GameStateEnum.EndCutsceneWin;
				}
				else
				{
					CreateTween().TweenInterval(2).Finished += () => Global.GameState = GameStateEnum.EndCutsceneLose;
				}
			}
		};

		Global.OnGameStateChangedDelegate += OnGamestateChanged;

	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("B Controller"))
		{
			Pry.ButtonPressed = true;
			KillButton.ButtonPressed = true;
		}
		if (@event.IsActionPressed("A Controller"))
		{
			DontPry.ButtonPressed = true;
		}
	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		PryContainer.Hide();
		KillContainer.Hide();
		CutsceneContainer.Hide();
		if (gameState == GameStateEnum.TalkingPhase)
		{
			Reset();
			PryContainer.Show();
		}
		if (gameState == GameStateEnum.KillingPhase)
		{
			Reset();
			KillContainer.Show();
		}
		if (gameState == GameStateEnum.IntroCutscene)
		{
			Reset();
			CutsceneContainer.Show();
		}
	}


	public void Reset()
	{
		Pry.Disabled = true;
		DontPry.Disabled = true;

		DialogueContainer.Modulate = DialogueContainer.Modulate with { A = 0 };

		ResetDialogue();
		MenuOpen = false;
	}

	public void ResetDialogue()
	{
		DialougeLabel.Text = "";
		DialougeLabel.VisibleRatio = 0;
		KillLabel.VisibleRatio = 0;
		CutsceneLabel.VisibleRatio = 0;
		dialogieInProcess = false;

		DontPry.Modulate = DontPry.Modulate with { A = 0 };
		Pry.Modulate = Pry.Modulate with { A = 0 };
		Pry.Disabled = true;
		DontPry.Disabled = true;


	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void PrepareNpcDialogue(Npc npc)
	{
		MenuOpen = true;
		ActiveNpc = npc;
		var tween = CreateTween();
		tween.TweenProperty(DialogueContainer, "modulate:a", 1, duration);
		if (Global.GameState == GameStateEnum.TalkingPhase)
		{
			tween.Finished += NpcDialogue;
		}
		if (Global.GameState == GameStateEnum.KillingPhase)
		{
			tween.Finished += ShowKillDialoge;
		}
	}

	public Tween PrepareCutscene()
	{
		MenuOpen = true;
		var tween = CreateTween();
		tween.TweenProperty(DialogueContainer, "modulate:a", 1, duration);
		return tween;
	}

	public void NpcDialogue()
	{
		string text;
		if (ActiveNpc.InteractionStatus == InteractionStatusEnum.Interacted)
		{
			text = ActiveNpc.IgnoreLines[ActiveNpc.lineIndex++ % ActiveNpc.IgnoreLines.Count];
		}
		else
		{
			text = ActiveNpc.Lines[ActiveNpc.lineIndex++ % ActiveNpc.Lines.Count];
		}
		ResetDialogue();
		ShowDialogue(text);
	}

	public void ShowDialogue(string text)
	{
		if (dialogieInProcess)
		{
			return;
		}
		dialogieInProcess = true;

		var tween = CreateTween();
		tween.TweenProperty(DialougeLabel, "visible_ratio", 1, duration);

		tween.TweenProperty(DontPry, "modulate:a", 1, duration);
		tween.Parallel().TweenProperty(Pry, "modulate:a", 1, duration / 2);
		tween.Finished += () =>
		{
			dialogieInProcess = false;

			Pry.Disabled = false;
			DontPry.Disabled = false;
			Pry.GrabFocus();
		};
		DialougeLabel.Text = text;
	}

	public void ShowCutsceneDialogue(string text)
	{
		ResetDialogue();
		if (dialogieInProcess)
		{
			return;
		}
		dialogieInProcess = true;

		var tween = CreateTween();
		tween.TweenProperty(CutsceneLabel, "visible_ratio", 1, text.Length / 50f);
		tween.Finished += () =>
		{
			dialogieInProcess = false;
		};
		CutsceneLabel.Text = text;
	}


	public void ShowKillDialoge()
	{
		if (dialogieInProcess)
		{
			return;
		}
		var tween = CreateTween();
		tween.TweenProperty(KillLabel, "visible_ratio", 1, duration);

		tween.Finished += () =>
		{
			dialogieInProcess = false;
			KillButton.GrabFocus();
		};
	}

	internal void HideDialogue()
	{
		Reset();
	}

}

using System;
using Godot;

public partial class DialogueUI : Control, IResetable
{
	VBoxContainer DialogueContainer => GetNode<VBoxContainer>("%DialogueContainer");
	Label DialougeLabel => GetNode<Label>("%DialougeLabel");
	RichTextLabel KillLabel => GetNode<RichTextLabel>("%KillLabel");
	TextureButton KillButton => GetNode<TextureButton>("%KillButton");

	HBoxContainer PryContainer => GetNode<HBoxContainer>("%PryContainer");
	HBoxContainer KillContainer => GetNode<HBoxContainer>("%KillContainer");

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
			Pry.Disabled = true;
			DontPry.Disabled = true;
			NpcDialogue();
		};
		DontPry.Pressed += () =>
		{
			Pry.Disabled = true;
			DontPry.Disabled = true;
			ActiveNpc.InteractionStatus = InteractionStatusEnum.Interacted;
			Reset();
		};

		KillButton.Pressed += () =>
		{
			ActiveNpc.OnKilled();
			Global.GameState = GameStateEnum.KillingSelected;
			CreateTween().TweenInterval(1).Finished += () => Global.GameState = GameStateEnum.End;
			CreateTween().TweenInterval(3).Finished += () => Global.GameState = GameStateEnum.TalkingPhase;
		};

		Global.OnGameStateChangedDelegate += OnGamestateChanged;

	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		PryContainer.Hide();
		KillContainer.Hide();
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
	}


	public void Reset()
	{
		DialogueContainer.Modulate = DialogueContainer.Modulate with { A = 0 };

		ResetDialogue();
		MenuOpen = false;
	}

	public void ResetDialogue()
	{
		DialougeLabel.Text = "";
		DialougeLabel.VisibleRatio = 0;
		KillLabel.VisibleRatio = 0;

		dialogieInProcess = false;

		DontPry.Modulate = DontPry.Modulate with { A = 0 };
		Pry.Modulate = Pry.Modulate with { A = 0 };

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
		};
		DialougeLabel.Text = text;
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
		};
	}

	internal void HideDialogue()
	{
		Reset();
	}

}

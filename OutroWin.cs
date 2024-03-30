using Godot;
using System;
using System.Collections.Generic;

public partial class OutroWin : PanelContainer
{
	List<string> conversation = new List<string>
		{
			"[center][b]Devil:[/b] Congratulations on your win, programmer. Now, it's time for you to fulfill your end. Help my grandmother with her email client.[/center]",
			"[center][b]Programmer:[/b] What? I have to help your grandmother? That's absurd.[/center]",
			"[center][b]Devil:[/b] It's part of our deal. Do it, or suffer the consequences.[/center]",
			"[center]Reluctantly, the programmer assists the devil's grandmother, grumbling all the while about the inconvenience.[/center]",
			"[center][b]Grandmother:[/b] Oh, thank you, dear. You're such a lifesaver![/center]",
			"[center][b]Programmer:[/b] (Under his breath) More like a soul-saver.[/center]"
		};

	public int index;
	public TextureButton advance => GetNode<TextureButton>("AdvanceButton");
	public override void _Ready()
	{
		Global.OnGameStateChangedDelegate += OnGamestateChanged;
		advance.Pressed += () =>
		{
			if (index == conversation.Count)
			{
				Global.GameState = GameStateEnum.End;
			}
			else
			{
				Global.DialogueUI.ShowCutsceneDialogue(conversation[index++]);
			}
		};
	}

	private void OnGamestateChanged(GameStateEnum gameState)
	{
		if (gameState == GameStateEnum.EndCutsceneWin)
		{
			Show();
			Global.DialogueUI.PrepareCutscene().Finished += () => Global.DialogueUI.ShowCutsceneDialogue(conversation[index++]);
			advance.Disabled = false;
		}
		else
		{
			advance.Disabled = true;
			Hide();
		}
	}

}

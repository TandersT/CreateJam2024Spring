using Godot;
using System;
using System.Collections.Generic;

public partial class OutroLose : PanelContainer
{
	List<string> conversation = new List<string>
		{
			"[center][b]Programmer:[/b] You said you'd help me win the game jam, Devil! Look where that got me – dead last![/center]",
			"[center][b]Devil:[/b] My sincerest apologies, dear programmer. It seems my expertise in gaming isn't as devilishly effective as I'd hoped.[/center]",
			"[center][b]Programmer:[/b] No kidding! I could've coded better results in my sleep. What do you have to say for yourself?[/center]",
			"[center][b]Devil:[/b] Ah, but remember, even in defeat, there are valuable lessons to be learned. Plus, we can always try again.[/center]"
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
				Global.MenuState = MenuStateEnum.Menu;
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
		if (gameState == GameStateEnum.EndCutsceneLose)
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
		if (gameState == GameStateEnum.Idle)
		{
			index = 0;
		}
	}

}

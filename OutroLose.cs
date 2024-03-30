using Godot;
using System;
using System.Collections.Generic;

public partial class OutroLose : PanelContainer
{
	List<string> conversation = new List<string>
		{
			"[center][b]Devil:[/b] Ah, weary coder, it seems the winds of fate have not blown in your favor. The game jam slips from your grasp like sand through an hourglass.[/center]",
			"[center][b]Programmer:[/b] Alas, devil, my efforts were not enough to seize victory. The bugs proved too elusive, the glitches too stubborn. My soul hangs in the balance, yet victory remains beyond reach.[/center]",
			"[center][b]Devil:[/b] A pity, indeed. But fret not, for failure is but a temporary setback. A new opportunity may yet arise, ripe for our arrangement.[/center]",
			"[center][b]Programmer:[/b] Your words offer little solace, devil. The weight of my defeat bears heavy upon me. What price must I pay to turn the tide in my favor?[/center]",
			"[center][b]Devil:[/b] Patience, dear coder. Our pact remains intact, awaiting the moment when fortune smiles upon us once more. Until then, rest assured that the devil's door is always open to those in need.[/center]"
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
	}

}

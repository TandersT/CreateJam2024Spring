using Godot;
using System;
using System.Collections.Generic;

public partial class Intro : PanelContainer
{
	List<string> conversation = new List<string>
		{
			"[center][b]Devil:[/b] Ah, a weary soul, cloaked in the glow of a monitor. What keeps you up at this hour, [b]programmer[/b]?[/center]",
			"[center][b]Programmer:[/b] I'm on the brink of failure, my code tangled in the webs of bugs and glitches. The game jam is slipping from my grasp.[/center]",
			"[center][b]Devil:[/b] A game jam, you say? A tempting challenge indeed. But tell me, why seek the aid of a devil?[/center]",
			"[center][b]Programmer:[/b] Desperation breeds unlikely alliances. I need to win, to prove my worth, to secure my future in this industry.[/center]",
			"[center][b]Devil:[/b] And what would you offer in exchange for such victory?[/center]",
			"[center][b]Programmer:[/b] My soul, perhaps? Though it's not worth much these days, buried beneath lines of code.[/center]",
			"[center][b]Devil:[/b] Souls are mundane currency, my dear coder. But how about this: I grant you the brilliance to conquer this jam, and in return, you'll do my bidding when I call upon you.[/center]",
			"[center][b]Programmer:[/b] What kind of bidding?[/center]",
			"[center][b]Devil:[/b] Oh, nothing too taxing, just the occasional favor. A line of code here, a tweak there. You'll hardly notice the difference.[/center]",
			"[center][b]Programmer:[/b] Very well. I accept your terms, [b]devil[/b]. Grant me the power to emerge victorious in this game jam.[/center]",
			"[center][b]Devil:[/b] As you wish, [b]programmer[/b]. But remember, deals with the devil always come due.[/center]"
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
				Global.GameState = GameStateEnum.RoundFinished;
			}
			else
			{
				Global.DialogueUI.ShowCutsceneDialogue(conversation[index++]);
			}
		};
	}



	private void OnGamestateChanged(GameStateEnum gameState)
	{
		if (gameState == GameStateEnum.IntroCutscene)
		{
			Show();
			Global.DialogueUI.PrepareCutscene().Finished += () => Global.DialogueUI.ShowCutsceneDialogue(conversation[index++]);
			advance.Disabled = false;
			advance.GrabFocus();
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

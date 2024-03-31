using Godot;
using System;

public partial class UIController : Control
{

	Control GameUI => GetNode<Control>("GameUI");
	Control MenuUI => GetNode<Control>("MenuUI");
	public override void _Ready()
	{
		Global.OnMenuStateChangedDelegate += GameStateChanged;
	}

    private void GameStateChanged(MenuStateEnum menuState)
    {
		MenuUI.Hide();
		GameUI.Hide();
		
        switch (menuState)
        {
            case MenuStateEnum.NaN:
                break;
            case MenuStateEnum.Menu:
				MenuUI.Show();
                break;
            case MenuStateEnum.About:
                break;
            case MenuStateEnum.Readme:
                break;
            case MenuStateEnum.End:
                break;
            case MenuStateEnum.Settings:
                break;
            case MenuStateEnum.Ingame:
				GameUI.Show();
                break;
            case MenuStateEnum.Pause:
				MenuUI.Show();
				GameUI.Show();
                break;
        }

    }
}
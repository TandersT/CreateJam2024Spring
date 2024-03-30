using Godot;
using System;

public partial class Main : Node2D
{
	[Export]
	public bool Quickstart = false;
	public override void _Ready()
	{
		if (Quickstart)
		{
			Global.GameState = GameStateEnum.Active;
			Global.MenuState = MenuStateEnum.Ingame;
		}
		else
		{
			Global.GameState = GameStateEnum.Idle;
			Global.MenuState = MenuStateEnum.Menu;
		}
	}
}

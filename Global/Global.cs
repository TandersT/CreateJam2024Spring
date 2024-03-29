using Godot;
using System;
using System.Collections.Generic;

public enum GameStateEnum
{
    NaN,
    Idle,
    Active,
    End,
}

public enum MenuStateEnum
{
    NaN,
    Menu,
    About,
    Readme,
    End,
    Settings,
    Ingame,

}

public static class Global
{
    public static int Score = 0;
    public delegate void GameStateChangedDelegate(GameStateEnum gameState);
    public static event GameStateChangedDelegate OnGameStateChangedDelegate;
    private static GameStateEnum gameState = GameStateEnum.NaN;
    public static GameStateEnum GameState
    {
        get { return gameState; }
        set
        {
            gameState = value;
            OnGameStateChangedDelegate?.Invoke(value);
        }
    }

    public delegate void MenuStateChangedDelegate(MenuStateEnum menuState);
    public static event MenuStateChangedDelegate OnMenuStateChangedDelegate;
    private static MenuStateEnum menuState = MenuStateEnum.NaN;
    public static MenuStateEnum MenuState
    {
        get { return menuState; }
        set
        {
            menuState = value;
            OnMenuStateChangedDelegate?.Invoke(value);
        }
    }

    
    public delegate void InteractDelegate(Npc npc);
    public static event InteractDelegate OnInteractDelegate;
    public static void OnInteract (Npc value) =>OnInteractDelegate?.Invoke(value);

    public static List<Npc> AllNpcs =  new List<Npc>();

    public static Player Player; 
}

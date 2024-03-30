using Godot;
using System;
using System.Collections.Generic;

public enum GameStateEnum
{
    NaN,
    Idle,
    IntroCutscene,
    TalkingPhase,
    KillingPhase,
    KillingSelected,
    EndCutsceneWin,
    EndCutsceneLose,
    End,
    RoundFinished,
    RoundStarted,

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
    public static List<string> DaysLeft = new()
    {
        "40 hours left",
        "24 hours left",
        "8 hours left",
    };

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
    public static List<Vector2> SpawnablePositions = new List<Vector2>();

    public static float MaxDistanceToNpc { get; internal set; } = 20000f;
    public static double RoundDuration { get; internal set; } = 20;
    public static int RoundCount = 0;
    public static DialogueUI DialogueUI { get; internal set; }
    public static SkillContainer SkillContainer { get; internal set; }
    public static NpcContainer NpcContatainer { get; internal set; }
    public static GameUI GameUI { get; internal set; }
    public static int MaxRoundCount { get; internal set; } = 3;


    public delegate void InteractDelegate(Npc npc);
    public static event InteractDelegate OnInteractDelegate;
    public static void OnInteract(Npc value) => OnInteractDelegate?.Invoke(value);

    public static List<Npc> AllNpcs = new List<Npc>();

    public static Player Player;

    public static PeasentRole PeasentRole = new();
    public static ProgrammerRole ProgrammerRole = new();
    public static SoundDesignerRole SoundDesignerRole = new();
    public static GraphicsArtistRole GraphicsArtistRole = new();

}

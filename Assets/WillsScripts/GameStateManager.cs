using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState // game states
    {
        MainMenu,
        Game,
        GameOver,
        GameWin,
        Pause,
        Settings,
        Credits,
        Controls
    }
    public GameState currentGameState;//current game state


}

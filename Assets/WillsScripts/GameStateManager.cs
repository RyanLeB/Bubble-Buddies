using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
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
    [SerializeField] private GameState currentGameState;
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameStateManager gameStateManager;//game state manager
    [SerializeField] private LevelManager levelManager;//level manager
    [SerializeField] private Singleton singleton;//singleton
    [SerializeField] private UIManager uiManager;//UI manager
    [SerializeField] private GameObject player;//player game object
    public SoundManager soundManager;//sound manager
    [Header("Variables")]
    public bool isPaused;//is the game paused

    // Start is called before the first frame update
    void Start()
    {
        soundManager.PlayMusic("BubbleBuddies");
        player = GameObject.FindGameObjectWithTag("Player");
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        singleton = GameObject.Find("Singleton").GetComponent<Singleton>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    void Update()
    {
        if(levelManager.scenename == "Game")
        {
            player.SetActive(true);
        }
        else
        {
            player.SetActive(false);
        }
    }
    /// <summary>
    /// Pause the game and set the UI to the pause menu
    /// </summary>
    public void PauseGame()
    {
        if(!isPaused)
        {
            isPaused = true;
            gameStateManager.currentGameState = GameStateManager.GameState.Pause;
            uiManager.SetUI("Pause");
            Time.timeScale = 0;
        }
        else if(isPaused)
        {
            isPaused = false;
            gameStateManager.currentGameState = GameStateManager.GameState.Game;
            uiManager.SetUI("GameUI");
            Time.timeScale = 1;
        }
    }
}

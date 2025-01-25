using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject pauseMenu;//Pause menu object
    [SerializeField] private GameObject gameOverMenu;//Game over menu object
    [SerializeField] private GameObject gameWinMenu;//Game win menu object
    [SerializeField] private GameObject gameUI;//Game UI object
    [SerializeField] private GameObject MainMenu;//Main menu object
    [SerializeField] private GameObject settingsMenu;//Settings menu object
    [SerializeField] private GameObject creditsMenu;//Credits menu object
    [SerializeField] private GameObject controlsMenu;//Controls menu object
    [Header("Class Calls")]
    [SerializeField] private GameplayTracker gameplayTracker;//Gameplay tracker object
    [SerializeField] private LevelManager levelManager;//Level manager object
    [Header("Win Stats")]
    [SerializeField] private GameObject[] survivingBubbles;//Bubble object
    [SerializeField] private int bubblesSaved;//Bubbles saved
    [SerializeField] private int bubblesLost;//Bubbles lost
    [SerializeField] private TextMeshProUGUI bubblesSavedText;//Bubbles saved text
    [SerializeField] private TextMeshProUGUI bubblesLostText;//Bubbles lost text

    [Header("Game State")]
    [SerializeField] private GameStateManager gameStateManager;//Game state manager object
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        SetUI("MainMenu");
    }
    void Update()
    {
        if(levelManager.scenename == "Game")
        {
            gameplayTracker = FindObjectOfType<GameplayTracker>();
        }
        survivingBubbles = GameObject.FindGameObjectsWithTag("Bubble");
    }

    /// <summary>
    /// Set the all of the UI to false
    /// </summary>
    void SetUIFalse()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinMenu.SetActive(false);
        gameUI.SetActive(false);
        MainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
    /// <summary>
    /// Set the UI to the string that is passed in
    /// </summary>
    /// <param name="UI"></param>
    public void SetUI(string UI)
    {
        //Set all UI to false
        SetUIFalse();
        //Switch statement to set the UI to true
        switch (UI)
        {
            case "Pause":
                pauseMenu.SetActive(true);
                break;
            case "GameOver":
                gameOverMenu.SetActive(true);
                break;
            case "GameWin":
                gameWinMenu.SetActive(true);
                break;
            case "GameUI":
                gameUI.SetActive(true);
                break;
            case "MainMenu":
                MainMenu.SetActive(true);
                break;
            case "Settings":
                settingsMenu.SetActive(true);
                break;
            case "Credits":
                creditsMenu.SetActive(true);
                break;
            case "Controls":
                controlsMenu.SetActive(true);
                break;
            case "Options":
                settingsMenu.SetActive(true);
                break;
            case null:
                Debug.Log(UI + " is null");
                break;
        }
    }
    public void WinGame()
    {
        SetUI("GameWin");
        gameplayTracker.BubbleTracker();
        bubblesSavedText.text = "Bubbles Saved: " + gameplayTracker.bubblesSaved;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameManage gameManage;
    public LevelTransition levelTransition;
    
    public string scenename;
    void Start()
    {
        gameManage = FindObjectOfType<GameManage>();
    }
    void Update()
    {
        if(scenename == "Game")
        {
            levelTransition = GameObject.FindGameObjectWithTag("LevelTransition").GetComponent<LevelTransition>();
        }
    }
    /// <summary>
    /// Load the scene that is passed in
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public void LoadScene(string sceneName)
    {
        scenename = sceneName;
        switch(sceneName)
        {
            case "MainMenu":
                SceneManager.LoadScene("MainMenuScene");
                gameManage.soundManager.PlayMusic("BubbleBuddies");
                break;
            case "Game":
                SceneManager.LoadScene("Game");
                //levelTransition.ReloadLevel();
                break;
            case "Clicker":
                SceneManager.LoadScene("Interaction Main Menu");
                gameManage.soundManager.PlayMusic("ClickingGame");
                break;
            case "GameOver":
                SceneManager.LoadScene("GameOver");
                break;
        }
    }
    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

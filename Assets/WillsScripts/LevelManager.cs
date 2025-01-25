using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Load the scene that is passed in
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public void LoadScene(string sceneName)
    {
        switch(sceneName)
        {
            case "MainMenu":
                SceneManager.LoadScene("MainMenuScene");
                break;
            case "Game":
                SceneManager.LoadScene("Game");
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

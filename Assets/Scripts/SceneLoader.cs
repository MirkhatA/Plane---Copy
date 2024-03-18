using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(1);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

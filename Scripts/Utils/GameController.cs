using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    protected string sceneName = "MainMenu";
    private void Start()
    {
        Time.timeScale = 1f;
    }


    public void StartGame()
    {
        Time.timeScale = 1f;
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(this.sceneName);
    }
}

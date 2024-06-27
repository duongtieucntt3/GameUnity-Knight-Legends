using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Texture2D cursorArrow;
    private string ctnScene;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Button restartFromSaveFile;
    [SerializeField] GameObject tutorialsPanel;
    [SerializeField] Slider volumeSlider;

    public void NewGame()
    {
        PlayerPrefs.SetString("newGame", "true");
        SceneManager.LoadScene("Intro");
    }

    public void Continue()
    {
        PlayerPrefs.SetString("newGame", "false");
        if (ctnScene != null)
            SceneManager.LoadScene(ctnScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Tutorial()
    {
        mainPanel.SetActive(false);
        tutorialsPanel.SetActive(true);
    }
    public void Main()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        tutorialsPanel.SetActive(false );
    }

    public void Options()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
       
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public Texture2D cursorArrow;
    public Texture2D cursorArrow2;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject GameOver;

    public GameObject playerObject;
    private PlayerMovement playerScript;
    private PlayerCombat playerCombat;
    private Bow playerBow;

    public static bool GameIsPaused = false;
    public static bool inOptions = false;

    private void Update()
    {
        if (GameOver.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    if (!inOptions)
                        Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        Cursor.SetCursor(cursorArrow2, Vector2.zero, CursorMode.ForceSoftware);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Invoke(nameof(EnableInputs), 0.1f);
    }

    public void Pause()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        pauseMenuUI.SetActive(true);
        playerBow.InputDisabled = true;
        playerCombat.InputDisabled = true;
        playerScript.isDisabled = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void EnableInputs()
    {
        playerBow.InputDisabled = false;
        playerCombat.InputDisabled = false;
        playerScript.isDisabled = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void Main()
    {
        inOptions = false;
        pauseMenuUI.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void Options()
    {
        inOptions = true;
        pauseMenuUI.SetActive(false);
        optionsPanel.SetActive(true);
    }

    
    [SerializeField] Slider volumeSlider;
    

    private void Awake()
    {
        playerScript = playerObject.GetComponent<PlayerMovement>();
        playerCombat = playerObject.GetComponent<PlayerCombat>();
        playerBow = playerObject.GetComponent<Bow>();

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

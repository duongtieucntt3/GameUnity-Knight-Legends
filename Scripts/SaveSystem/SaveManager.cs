using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public string currentScene;
    #region public objects
    public GameObject healthControllerContainer;
    public GameObject moneySystemContainer;
    public GameObject menuContainer;
    #endregion
    #region private scripts
    private HealthController healthController;
    private MoneySystem moneySystem;
    private PauseMenuScript menu;
    #endregion
    private GameData savedData;
    private bool loadSuccess = true;

    public void Save()
    {
        var data = new GameData();
        data.currentScene = currentScene;
        if (PlayerPrefs.HasKey("Volume"))
        {
            data.volume = PlayerPrefs.GetFloat("Volume");
        }

        data.health = healthController.currentHealth;
        data.healthPotions = healthController.potions;
        data.maxHealth = healthController.MaxHealth;
        data.coins = moneySystem.coins;

        SaveWorker.SaveGame(data);
    }

    private void Load()
    {
        GameData data = SaveWorker.Load();
        savedData = data;
        LoadData();
    }

    private void LoadData()
    {
        if (savedData != null)
        {
            Debug.Log("loading");
            healthController.currentHealth = savedData.health;
            healthController.MaxHealth = savedData.maxHealth;
            healthController.potions = savedData.healthPotions;
            loadSuccess = healthController.UpdateData();
            moneySystem.coins = savedData.coins;
            moneySystem.UpdateData();
           
            menu.SetVolume(savedData.volume);
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        healthController = healthControllerContainer.GetComponent<HealthController>();
        moneySystem = moneySystemContainer.GetComponent<MoneySystem>();
        menu = menuContainer.GetComponent<PauseMenuScript>();

        if (SaveWorker.SaveExists() && PlayerPrefs.GetString("newGame") == "false")
        {
            Load();
        }
        if(currentScene == "FirstScene")
        {
            PlayerPrefs.SetString("newGame", "false");
        }

        Save();
    }

    private void Update()
    {
        if (!loadSuccess)
        {
            LoadData();
        }
    }
}

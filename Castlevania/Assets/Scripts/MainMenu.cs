using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject levelsMenu;
    public Text[] level1Records;
    public Text[] level2Records;
    public GameObject level2Button;
    public GameObject recordsMenu;

    private LevelData levelData;

    private void Start()
    {
        levelData = SaveManager.Read("Save.dat");
    }

    private void Update()
    {
        level2Button.SetActive(levelData.accessToLevels[1]);
        for (int i = 0; i < 10; i++)
        {
            level1Records[i].text = levelData.levelSRecords[0, i].ToString();
            level2Records[i].text = levelData.levelSRecords[1, i].ToString();
        }
    }

    public void NewGame()
    {
        levelData.gameStarted = true;
        SaveManager.Save("Save.dat", levelData);
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        if (levelData.gameStarted)
        {
            SceneManager.LoadScene(levelData.activeLevel);
        }
    }

    public void Levels()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(true);
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void Records()
    {
        mainMenu.SetActive(false);
        recordsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelsMenu.SetActive(false);
        recordsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

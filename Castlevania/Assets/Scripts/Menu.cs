using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private bool paused;

    public GameObject PauseUI;
    public GameObject DeathUI;
    public GameObject VictoryUI;
    public PlayerCntrl player;

    private GameMaster gm;

	void Start () {
        PauseUI.SetActive(false);
        DeathUI.SetActive(false);
        VictoryUI.SetActive(false);
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
	
	void Update () {
		if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (gm.PlayerWon)
        {
            VictoryUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            DeathUI.SetActive(player.Dead);
            Time.timeScale = System.Convert.ToSingle(!player.Dead);

            PauseUI.SetActive(paused && !player.Dead);
            if (!player.Dead)
            {
                Time.timeScale = System.Convert.ToSingle(!paused);
            }
        }
	}

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           
    }
}

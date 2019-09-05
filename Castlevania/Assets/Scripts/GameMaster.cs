using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public bool PlayerWon { get; private set; }
    public int Score { get; set; }
    private int bonusCounter = 0;
    public int leftLimit;
    public int rightLimit;
    public int leftLimitBossFight;

    public GameObject[] bonuses;
    public GameObject stone;
    private GameObject usedBonus;

    public PlayerCntrl player;
    public Enemy boss;
    private LevelData levelData;

    void Start()
    {
        levelData = SaveManager.Read("Save.dat");
        SaveCurrentLevel();
        bonusCounter = 0;
        Score = 0;
        PlayerWon = false;
        player.transform.position = new Vector2(10, 2);
        player.Health = 5;
    }

    void Update()
    {
        CheckGame();
    }

    private void CheckGame()
    {
        CheckPlayerPosition();

        if (boss.Health <= 0 && !PlayerWon)
        {
            PlayerWon = true;
            SaveProgress();
        }
        if (player.Level == 2 && bonuses[2] != null)
        {
            bonuses[2] = null;
        }

        if (player.CurrentAdditionalWeapon == player.UseDagger)
        {
            if (bonuses[0] == null)
            {
                bonuses[0] = usedBonus;
            }
            usedBonus = bonuses[1];
            bonuses[1] = null;
        }
        else if (player.CurrentAdditionalWeapon == player.UseHolyWater)
        {
            if (bonuses[1] == null)
            {
                bonuses[1] = usedBonus;
            }
            usedBonus = bonuses[0];
            bonuses[0] = null;
        }
    }

    public void CheckPlayerPosition()
    {
        if (player.transform.position.x > leftLimitBossFight && leftLimit != leftLimitBossFight)
        {
            leftLimit = leftLimitBossFight;
        }

        if (player.transform.position.x < leftLimit)
        {
            player.transform.position = new Vector2(leftLimit, player.transform.position.y);
        } else if (player.transform.position.x > rightLimit)
        {
            player.transform.position = new Vector2(rightLimit, player.transform.position.y);
        }
    }

    public GameObject GetBonus()
    {
        GameObject bonusClone;

        if (bonusCounter % 5 == 0)
        {
            do
            {
                bonusClone = bonuses[(int)(Random.value * 2.99)];
            } while (bonusClone == null);
            bonusCounter -= 5;
        }
        else
        {
            bonusClone = stone;
        }
        bonusCounter++;
        return bonusClone;
    }

    private void SaveCurrentLevel()
    {
        levelData.activeLevel = SceneManager.GetActiveScene().buildIndex;
        SaveManager.Save("Save.dat", levelData);
    }

    private void SaveProgress()
    {
        if (SceneManager.GetActiveScene().buildIndex != levelData.accessToLevels.Length)
        {
            levelData.accessToLevels[SceneManager.GetActiveScene().buildIndex] = true;
        }
        CheckScore();
        SaveManager.Save("Save.dat", levelData);
    }

    private void CheckScore()
    {
        List<int> listOfScore = new List<int>();
        listOfScore.Add(Score);
        for(int i = 0; i < 10; i++)
        {
            listOfScore.Add(levelData.levelSRecords[SceneManager.GetActiveScene().buildIndex - 1, i]);
        }
        listOfScore.Sort();
        for (int i = 0; i < 10; i++)
        {
            levelData.levelSRecords[SceneManager.GetActiveScene().buildIndex - 1, i] = listOfScore[10 - i];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] HeartSprites;
    public Sprite[] AdditionalWeapons;

    public Image HeartsUI;
    public Image EnemyHealthUI;
    public Image AdditionalWeapon;
    public Text scoreText;
    public Text weaponCountText;

    public PlayerCntrl player;
    private Enemy boss;
    private GameMaster gm;

	void Start ()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Enemy>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
	
	void Update ()
    {
        if (player.CurrentAdditionalWeapon == player.UseDagger)
        {
            AdditionalWeapon.sprite = AdditionalWeapons[0];
            AdditionalWeapon.transform.localScale = new Vector2(2, 1);
        }
        else if (player.CurrentAdditionalWeapon == player.UseHolyWater)
        {
            AdditionalWeapon.sprite = AdditionalWeapons[1];
            AdditionalWeapon.transform.localScale = new Vector2(1, 1);
        }
        scoreText.text = "Score: " + gm.Score;
        weaponCountText.text = ": " + player.CountOfAdditionalWeapon;  
        if (boss.Awake == true)
        {
            EnemyHealthUI.enabled = true;
        }
        HeartsUI.sprite = HeartSprites[player.Health > 0 ? player.Health : 0];
        EnemyHealthUI.sprite = HeartSprites[(int)boss.Health];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDaggerState : IAdditionalWeapon
{
    private PlayerCntrl player;

    public UseDaggerState(PlayerCntrl player)
    {
        this.player = player;
    }

    public void UseWeapon()
    {
        GameObject daggerClone = Object.Instantiate(player.dagger, player.transform.position, Quaternion.identity);
        if (player.FacingRight)
        {
            daggerClone.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
        }
        else
        {
            daggerClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
            daggerClone.transform.localScale *= -1;
        }
    }
}

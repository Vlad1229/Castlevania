using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHolyWaterState : IAdditionalWeapon
{
    private PlayerCntrl player;

    public UseHolyWaterState(PlayerCntrl player)
    {
        this.player = player;
    }

    public void UseWeapon()
    {
        GameObject holyWaterClone = Object.Instantiate(player.holyWater, player.transform.position, Quaternion.identity);
        if (player.FacingRight)
        {
            holyWaterClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(700, 400));
        }
        else
        {
            holyWaterClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-700, 400));
        }
    }
}

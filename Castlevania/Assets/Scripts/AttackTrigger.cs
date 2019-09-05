using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public float damage;

    public PlayerCntrl player;

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            collision.SendMessageUpwards("Open");
        }
        else if (collision.CompareTag("EnemyWeapon"))
        {
            Destroy(collision.gameObject);
        }
    }
}

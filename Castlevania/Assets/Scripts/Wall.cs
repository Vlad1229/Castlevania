using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.transform.position.x > transform.position.x)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}

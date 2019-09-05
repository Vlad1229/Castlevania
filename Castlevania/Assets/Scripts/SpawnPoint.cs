using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    private float spawnCd = 2f;
    private float spawmTimer = 0f;

    public Transform player;
    public GameObject undead;

    void FixedUpdate ()
    {
        spawmTimer -= Time.deltaTime;
		if ((transform.position.x - player.transform.position.x) > 15 && spawmTimer <= 0)
        {
            Instantiate(undead, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            spawmTimer = spawnCd;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy {

    private float createSkeletonCd = 3f;
    private float timer = 0;
    private float skeletonSpawnX;
    private float skeletonSpawnY;

    public GameObject skeleton;

	protected override void Start () {
        skeletonSpawnX = transform.position.x - 1;
        skeletonSpawnY = transform.position.y - 1;
        base.Start();
        Health = 3f;
    }

    protected override void Attack()
    {
        timer -= Time.deltaTime;
        Anim.SetBool("attacking", (timer > 2.5f || timer < 0.5f) && Awake);

        if (Awake && !Stunned && timer <= 0)
        {
            GameObject skeletonClone = Instantiate(skeleton, new Vector2(skeletonSpawnX, skeletonSpawnY), Quaternion.identity);
            if (!FacingRight)
            {
                skeletonClone.GetComponent<Skeleton>().ChangeDirection();
            }
            timer = createSkeletonCd;
        }

        if (transform.position.x < Player.transform.position.x && FacingRight)
        {
            Flip();
            skeletonSpawnX = transform.position.x + 1;
        }
        else if (transform.position.x > Player.transform.position.x && !FacingRight)
        {
            Flip();
            skeletonSpawnX = transform.position.x - 1;
        }
        Rb.velocity = new Vector2(-Rb.velocity.x, Rb.velocity.y);
    }
}

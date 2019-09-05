using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkeleton : Enemy
{
    public GameObject bone;
    private float range = 10;
    private float point;
    private float amplitude = 3f;
    private float distance;
    private float timer = 0;
    private float attackCd = 2;

    protected override void Start()
    {
        base.Start();
        Health = 2;
        Awake = true;
        point = transform.position.x;
    }

    protected override void Attack()
    {
        Rb.velocity = new Vector2(Speed, 0);
        timer -= Time.deltaTime;
        distance = Player.transform.position.x - transform.position.x;
        if (Mathf.Abs(distance) < range && timer <= 0)
        {
            GameObject boneClone = Instantiate(bone, transform.position, Quaternion.identity);
            boneClone.GetComponent<Rigidbody2D>().velocity = new Vector2(2*distance/3, 0);
            boneClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
            timer = attackCd;
        }

        if (Mathf.Abs(transform.position.x - point) > amplitude)
        {
            Flip();
            Speed = -Speed;
            point = transform.position.x;
        }
    }
}

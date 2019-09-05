using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy
{
    private Vector2 point;
    private Vector2 radius;
    private float radiusLength = 8;
    private float attackTimer = 1;
    private float attackCd = 5;
    private float countOfScythes = 4;

    public GameObject scytche;

    protected override void Start()
    {
        radius = new Vector2(radiusLength, 0);
        rangeOfSeeing = 15;
        base.Start();
        Speed = 1f;
        Health = 5;
        point = new Vector2(transform.position.x, transform.position.y) - radius;
    }

    protected override void Attack()
    {
        if (Awake)
        {
            attackTimer -= Time.deltaTime;
            radius = new Vector2(transform.position.x, transform.position.y) - point;
            Rb.velocity = new Vector2(-radius.y, radius.x) * Speed;
            if (radius.y <= 0 && (Speed > 0 && radius.x < 0 || Speed < 0 && radius.x > 0))
            {
                point.x = transform.position.x - Mathf.Sign(radius.x) * radiusLength;
                Speed = -Speed;
            }

            if (attackTimer <= 0)
            {
                for (int i = 0; i < countOfScythes; i++)
                {
                    Vector2 position = new Vector2(Random.value * 15 + point.x - radiusLength, Random.value * 5 + 6);
                    GameObject scytcheClone = Instantiate(scytche, position, Quaternion.identity);
                    Vector2 direction = Player.transform.position - scytcheClone.transform.position;
                    scytcheClone.GetComponent<Rigidbody2D>().velocity = direction.normalized * 3;
                }
                attackTimer = attackCd;
            }
        }
    }
}

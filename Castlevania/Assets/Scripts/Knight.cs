using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    public GameObject axe;
    private float range = 10;
    private float point;
    private float amplitude = 3f;
    private float distance;
    private float timer = 0;
    private float attackCd = 2;

    protected override void Start()
    {
        base.Start();
        point = transform.position.x;
        Health = 2;
    }

    protected override void Attack()
    {
        Rb.velocity = new Vector2(Speed, 0);
        timer -= Time.deltaTime;
        distance = Player.transform.position.x - transform.position.x;
        if (Mathf.Abs(distance) < range && timer <= 0 && (distance <= 0 && FacingRight || distance >= 0 && !FacingRight))
        {
            GameObject axeClone = Instantiate(axe, transform.position, Quaternion.identity);
            axeClone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(distance) * 10, 0);
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

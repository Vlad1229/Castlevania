using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

    private int direction = 1;

	protected override void Start ()
    {
        base.Start();
        Awake = true;
    }

    public void ChangeDirection()
    {
        direction = -direction;
        Flip();
    }

    protected override void Attack()
    {
        if (Awake && !Stunned)
        {
            Rb.velocity = new Vector2(Speed * direction, Rb.velocity.y);
        }
    }
}

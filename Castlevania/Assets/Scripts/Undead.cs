using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : Enemy {

    protected override void Start()
    {
        base.Start();
        Awake = true;
    }

    protected override void Attack()
    {
        if (Awake && !Stunned)
        {
            Rb.velocity = new Vector2(Speed, Rb.velocity.y);
        }
    }
}

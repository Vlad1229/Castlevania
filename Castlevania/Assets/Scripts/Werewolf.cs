using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : Enemy
{
    private bool jump = false;
    private float jumpCd = 4f;
    private float jumpTimer = 1f;
    private bool grounded = false;
    private float groundRadius = 0.2f;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (Awake)
        {
            jumpTimer -= Time.deltaTime;
        }

        Anim.SetBool("awake", Awake);
        Anim.SetBool("grounded", grounded);

        if (Awake && !Stunned)
        {
            if (grounded)
            {
                jump = false;

                if (!jump && jumpTimer <= 0.3f)
                {
                    Anim.SetBool("prepare", true);
                }
                if (!jump && jumpTimer <= 0)
                {
                    Anim.SetBool("prepare", false);
                    Speed = Player.transform.position.x - Rb.position.x;
                    jumpTimer = jumpCd;
                    Rb.AddForce(new Vector2(0, 700));
                    jump = true;
                }
            }
            else
            {
                Rb.velocity = new Vector2(Speed, Rb.velocity.y);
            }
        }

        if (grounded)
        {
            Rb.velocity = new Vector2(-Rb.velocity.x, Rb.velocity.y);

            if (transform.position.x < Player.transform.position.x && FacingRight)
            {
                Flip();
            }
            else if (transform.position.x > Player.transform.position.x && !FacingRight)
            {
                Flip();
            }
        }
    }
}

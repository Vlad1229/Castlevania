using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastFormState : IVampireState
{
    private bool jump;
    private int countOfJumps = 4;
    private float jumpCd = 2f;
    private float jumpTimer = 0f;
    private bool grounded = false;
    private float groundRadius = 0.2f;
    private float jumpForce = 1000;

    private Vampire vampire;

    public BeastFormState(Vampire vampire)
    {
        this.vampire = vampire;
    }

    public void Attack()
    {
        grounded = Physics2D.OverlapCircle(vampire.groundCheck.position, groundRadius, vampire.whatIsGround);
        vampire.Anim.SetBool("grounded", grounded);

        jumpTimer -= Time.deltaTime;

        if (!vampire.Stunned)
        {
            if (grounded)
            {
                jump = false;
                vampire.Anim.SetBool("prepare", jumpTimer < 0.5f && jumpTimer > 0 && countOfJumps > 0);

                if (!jump && jumpTimer <= 0)
                {
                    if (countOfJumps > 0)
                    {
                        jump = true;
                        vampire.Speed = (vampire.Player.transform.position.x - vampire.Rb.position.x) * 2 / 3;
                        vampire.Rb.AddForce(new Vector2(0, jumpForce));
                        jumpTimer = jumpCd;
                        countOfJumps--;
                    }
                    else
                    {
                        vampire.Anim.SetBool("beastForm", false);
                        vampire.Anim.SetBool("humanForm", true);
                    }
                        
                }
            }
            else
            {
                vampire.Rb.velocity = new Vector2(vampire.Speed, vampire.Rb.velocity.y);
            }
        }

        if (grounded)
        {
            vampire.Rb.velocity = new Vector2(-vampire.Rb.velocity.x, vampire.Rb.velocity.y);

            if (vampire.transform.position.x < vampire.Player.transform.position.x && vampire.FacingRight)
            {
                vampire.Flip();
            }
            else if (vampire.transform.position.x > vampire.Player.transform.position.x && !vampire.FacingRight)
            {
                vampire.Flip();
            }
        }
    }

    public void ChangeState()
    {
        countOfJumps = 4;
        vampire.CurrentState = vampire.HumanForm;
        vampire.GetComponent<BoxCollider2D>().size = new Vector2(0.24f, 0.58f);
    }
}

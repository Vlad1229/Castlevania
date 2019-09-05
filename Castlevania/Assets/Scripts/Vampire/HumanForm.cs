using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanFormState : IVampireState
{
    private int countOfStrikes = 4;
    private float timer = 2;
    private float attackCd = 2f;
    private int speedOfFireBall = 5;

    private Vampire vampire;

    public HumanFormState(Vampire vampire)
    {
        this.vampire = vampire;
    }

    public void Attack()
    {
        timer -= Time.deltaTime;

        vampire.Anim.SetBool("attacking", (timer < 0.5 || timer > 1.5) && countOfStrikes > 0);

        if (countOfStrikes > 0 && timer <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject fireBallClone = GameObject.Instantiate(vampire.fireBall, vampire.transform.position, Quaternion.identity);
                float directionX = Mathf.Sign(vampire.Player.transform.position.x - vampire.transform.position.x);
                Vector2 speed = new Vector2(directionX * Mathf.Cos(i * Mathf.PI / 8), Mathf.Sin(i * Mathf.PI / 8)) * speedOfFireBall;
                fireBallClone.GetComponent<Rigidbody2D>().velocity = speed;
                timer = attackCd;
            }
            countOfStrikes--;
        }
        else if (countOfStrikes == 0 && timer <= 0)
        {
            vampire.Anim.SetBool("humanForm", false);
            vampire.Anim.SetBool("beastForm", true);
        }

        if (vampire.transform.position.x < vampire.Player.transform.position.x && vampire.FacingRight)
        {
            vampire.Flip();
        }
        else if (vampire.transform.position.x > vampire.Player.transform.position.x && !vampire.FacingRight)
        {
            vampire.Flip();
        }
    }

    public void ChangeState()
    {
        timer = 1;
        countOfStrikes = 4;
        vampire.CurrentState = vampire.BeastForm;
        vampire.GetComponent<BoxCollider2D>().size = new Vector2(0.48f, 0.64f);
    }
}

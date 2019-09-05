using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float Health { get; protected set; }
    public float Speed { get; set; }
    protected float stunTime = 0.4f;
    protected float speedX;
    protected float speedY;
    protected float rangeOfSeeing = 10;

    public bool Awake { get; protected set; }
    public bool Stunned { get; private set;}
    public bool FacingRight { get; private set; }
    
    public Rigidbody2D Rb { get; private set; }
    public GameObject Player { get; private set; }
    public Animator Anim { get; private set; }
    private GameMaster gm;

    protected virtual void Start ()
    {
        Player = GameObject.Find("player");
        Rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(10, 10);
        Anim = GetComponent<Animator>();
        Rb.velocity = new Vector2(0.1f, 0);
        Awake = false;
        Health = 1;
        Speed = -5;
        Stunned = false;
        FacingRight = true;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
	
	protected virtual void FixedUpdate ()
    {
        Check();
	}

    protected virtual void Check()
    {
        if (Mathf.Abs(Player.transform.position.x - Rb.position.x) < rangeOfSeeing)
        {
            Awake = true;
        }
        if ((Player.transform.position.x - Rb.position.x) > 20)
        {
            Destroy(gameObject);
            return;
        }
        Attack();
    }

    protected abstract void Attack();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Stunned && Awake && collision.CompareTag("Player"))
        {
            Health -= 1;
            if (Health <= 0)
            {
                Destroy(gameObject);
                gm.Score += 10;
                return;
            }
            Stun();
        }
    }

    protected virtual void Stun()
    {
        Stunned = true;
        speedX = Rb.velocity.x;
        speedY = Rb.velocity.y;
        Rb.isKinematic = true;
        Rb.velocity = new Vector2(0, 0);
        StartCoroutine(Unstun());
    }

    IEnumerator Unstun()
    {
        yield return new WaitForSeconds(stunTime);
        Stunned = false;
        Rb.isKinematic = false;
        Rb.velocity = new Vector2(speedX, speedY);
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

using System.Collections;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    private float speedAbs = 10f;
    private float speed = 0;
    private float jumpForce = 800f;
    public bool FacingRight { get; private set; }
    private bool grounded = false;
    private float groundRadius = 0.07f;
    private bool attacking = false;
    private bool stuned = false;
    private float attackTimer = 0;
    private float attackCd = 0.2f;
    private float additionalWeaponTimer = 0;
    private float additionalWeaponCd = 0.5f;

    public int Level { get; set; }
    private bool walking = false;
    public int Health { get; set; }
    public bool Dead { get; private set; }
    public int CountOfAdditionalWeapon { get; private set; }

    public UseDaggerState UseDagger { get; set; }
    public UseHolyWaterState UseHolyWater { get; set; }
    public IAdditionalWeapon CurrentAdditionalWeapon { get; set; }

    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Animator anim;
    public GameObject holyWater;
    public GameObject dagger;
    public Collider2D backCollider;
    public Collider2D frontCollider;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Health = 5;
        Dead = false;
        Level = 0;
        UseDagger = new UseDaggerState(this);
        UseHolyWater = new UseHolyWaterState(this);
        FacingRight = true;
        CountOfAdditionalWeapon = 0;
    }

    void FixedUpdate ()
    {
        Jump();
        Attack();
        Move();
    }

    void Attack()
    {
        float deltaTime = Time.deltaTime;
        attackTimer -= deltaTime;
        additionalWeaponTimer -= deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && !attacking && Health > 0 && attackTimer <= 0)
        {
            attacking = true;
            if (grounded && !stuned)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && Health > 0 && additionalWeaponTimer <= 0 && CountOfAdditionalWeapon > 0)
        {
            if (CurrentAdditionalWeapon != null)
            {
                CurrentAdditionalWeapon.UseWeapon();
                CountOfAdditionalWeapon -= 1;
            }
            additionalWeaponTimer = additionalWeaponCd;
        }

        anim.SetBool("attacking", attacking);
    }

    void Jump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !Dead)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
    }

    void Move()
    {
        walking = false;

        if (!stuned && !attacking)
        {
            Dead = Health <= 0;
            if (Input.GetKey(KeyCode.D))
            {
                speed = speedAbs;
                if (grounded)
                {
                    walking = true;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                speed = -speedAbs;
                if (grounded)
                {
                    walking = true;
                }
            }
            else
            {
                speed = 0;
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);
            if ((FacingRight && speed < 0) || (!FacingRight && speed > 0))
            {
                Flip();
            }
        }
        anim.SetBool("walking", walking);
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") || (coll.gameObject.CompareTag("Boss")))
        {
            Damage(1);
            if ((rb.position.x - coll.transform.position.x) <= 0)
            {
                rb.AddForce(new Vector2(-300, 500));
                if (!FacingRight)
                {
                    Flip();
                }
            }
            else
            {
                rb.AddForce(new Vector2(300, 500));
                if (FacingRight)
                {
                    Flip();
                }
            }
        }
        else if (coll.gameObject.CompareTag("Upgrade"))
        {
            LevelUp();
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("HolyWater"))
        {
            CurrentAdditionalWeapon = UseHolyWater;
            CountOfAdditionalWeapon += 5;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Dagger"))
        {
            CurrentAdditionalWeapon = UseDagger;
            CountOfAdditionalWeapon += 5;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Stone"))
        {
            CountOfAdditionalWeapon += 5;
            Destroy(coll.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            Damage(1);
            if ((rb.position.x - other.transform.position.x) <= 0)
            {
                rb.AddForce(new Vector2(-300, 500));
                if (!FacingRight)
                {
                    Flip();
                }
            }
            else
            {
                rb.AddForce(new Vector2(300, 500));
                if (FacingRight)
                {
                    Flip();
                }
            }
        }
    }

    void BackAttack()
    {   
        backCollider.enabled = true;
    }

    void FrontAttack()
    {
        backCollider.enabled = false;
        frontCollider.enabled = true;
    }

    void StopAttack()
    {
        frontCollider.enabled = false;
        attacking = false;
        attackTimer = attackCd;
    }

    void Damage(int damage)
    {
        Health -= damage;
        anim.SetBool("damaged", true);
        backCollider.enabled = false;
        frontCollider.enabled = false;
        attacking = false;
        Physics2D.IgnoreLayerCollision(9, 10);
        rb.velocity = new Vector2(0, 0);
        Stun();
        StartCoroutine(BackToLife());
    }

    void LevelUp()
    {
        if (Level < 2)
        {
            Level++;
            anim.SetInteger("level", Level);
            if (Level == 2)
            {
                frontCollider.transform.localScale = new Vector2(2 * frontCollider.transform.localScale.x, frontCollider.transform.localScale.y);
            }
        }
    }

    private void Stun()
    {
        stuned = true;
        StartCoroutine(Unstun());
    }

    IEnumerator Unstun()
    {
        yield return new WaitForSeconds(1);
        stuned = false;
    }

    IEnumerator BackToLife()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("damaged", false);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}

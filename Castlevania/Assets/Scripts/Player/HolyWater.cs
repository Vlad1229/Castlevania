using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    private bool grounded;
    private float groundRadius = 0.2f;
    private bool burning = false;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private BoxCollider2D coll;
    private Rigidbody2D rb;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (grounded)
        {
            Burn();
        }
    }

    void Burn()
    {
        burning = true;
        GetComponent<Animator>().SetBool("burning", burning);
        coll.size = new Vector2(1.44f, 0.31f);
        Physics2D.IgnoreLayerCollision(0, 10, false);
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector2(4, 4);
    }

    public void StopBurning()
    {
        Destroy(gameObject);
    }
}

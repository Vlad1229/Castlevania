using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public GameObject fireBall;

    public BeastFormState BeastForm { get; set; }
    public HumanFormState HumanForm { get; set; }
    public IVampireState CurrentState { get; set; }
    

    protected override void Start()
    {      
        BeastForm = new BeastFormState(this);
        HumanForm = new HumanFormState(this);
        CurrentState = HumanForm;
        rangeOfSeeing = 15;
        base.Start();
        Health = 5;
    }

    protected override void Attack()
    {
        if (Awake)
        {
            CurrentState.Attack();
        }
    }

    public void ChangeState()
    {
        CurrentState.ChangeState();
    }
}


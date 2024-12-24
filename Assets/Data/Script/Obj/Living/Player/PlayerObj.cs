using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerObj : LivingObj
{
    //==========================================Variable==========================================
    [Header("Player Obj")]
    // Stat
    [SerializeField] protected float moveSpeed;

    // Unity Component
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D bodyCollider;

    // Child Component
    [SerializeField] protected PlayerSO so;
    [SerializeField] protected Movement movement;
    [SerializeField] protected List<BaseWeapon> weapons;
    [SerializeField] protected BaseSkill characterSkill;



    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // SO
        string filePath = "SO/Living/Player/" + transform.name;
        this.LoadSO<PlayerSO>(ref this.so, filePath);
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()");
    }

    private void OnEnable()
    {
        this.LoadComponents();
        this.DefaultStat();
    }

    private void Update()
    {
        this.handleMove();
    }



    //==========================================Movement==========================================
    private void handleMove()
    {
        // Null Condition
        if (this.rb == null)
        {
            Debug.LogError("Rb is null", transform.gameObject);
            return;
        }

        InputManager input = InputManager.Instace;
        if (input == null)
        {
            Debug.LogError("Input is null", transform.gameObject);
            return;
        }

        // MoveDir Handle
        this.movement.MoveDir = Vector2.zero;
        if (Input.GetKeyDown(input.RightMove) || Input.GetKey(input.RightMove)) this.movement.MoveDir = new Vector2(1, this.movement.MoveDir.y);
        else if (Input.GetKeyDown(input.LeftMove) || Input.GetKey(input.LeftMove)) this.movement.MoveDir = new Vector2(-1, this.movement.MoveDir.y);

        if (Input.GetKeyDown(input.FrontMove) || Input.GetKey(input.FrontMove)) this.movement.MoveDir = new Vector2(this.movement.MoveDir.x, 1);
        else if (Input.GetKeyDown(input.BackMove) || Input.GetKey(input.BackMove)) this.movement.MoveDir = new Vector2(this.movement.MoveDir.x, -1);

        this.movement.Move();
    }

    //===========================================Weapon===========================================


    //======================================Character Skill=======================================


    //===========================================Other============================================
    private void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("SO is null", transform.gameObject);
            return;
        }

        // SO
        this.objName = this.so.name;
        this.moveSpeed = this.so.MoveSpeed;

        // Child Component
        this.movement = new Movement(this.rb);
        this.movement.MoveSpeed = this.moveSpeed;
    }
}

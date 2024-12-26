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

    private void FixedUpdate()
    {
        this.handleMove();
    }



    //==========================================Movement==========================================
    protected virtual void handleMove()
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

        // Move Handle
        this.movement.MoveDir = input.MoveDir;
        this.movement.Move();
    }

    //===========================================Weapon===========================================
    protected virtual void handleWeapon()
    {

    }

    //======================================Character Skill=======================================
    protected virtual void handleCharacterSkill()
    {

    }

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

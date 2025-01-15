using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crep : NPC, HpReceiver, ManaReceiver
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Crep===")]
    [Header("Move Random")]
    [SerializeField] protected RandomCooldown moveRandomCD;
    [SerializeField] protected Vector2 goalPos;
    [SerializeField] protected float randomRange;
    [SerializeField] protected bool isReachGoal;

    [Header("Weapon")]
    [SerializeField] protected Transform rightArm;
    [SerializeField] protected Weapon weapon;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rightArm, transform.Find("RightArm"), "LoadRightArm()");
        this.LoadChildComponent(ref this.weapon, transform.Find("WeaponHolder"), "LoadWeapon()");
        this.LoadSO(ref this.so, "SO/Character/NPC/Crep/" + transform.name);

        this.DefaultStat();
    }

    protected virtual void OnEnable()
    {
        this.canMove = true;
        this.isReachGoal = false;
        this.moveRandomCD.GetRandomDuration();
        this.goalPos = Util.Instance.RandomPos(transform.position, this.randomRange);
    }

    protected virtual void FixedUpdate()
    {
        this.Move();
        this.MoveRandomRecharge();
        this.CheckReachGoal();
        this.WeaponHandling();
        this.WeaponHolding();
    }    

    //========================================Move Random=========================================
    protected virtual void Move()
    {
        if (!this.canMove || this.isReachGoal) return;
        Vector2 moveDir = (this.goalPos - (Vector2)this.transform.position).normalized;
        this.rb.velocity = moveDir * this.moveSpeed;   
    }

    protected virtual void MoveRandomRecharge()
    {
        if (!this.isReachGoal) return;
        this.moveRandomCD.CoolingDown();

        if (!this.moveRandomCD.Cd.IsReady) return;
        this.moveRandomCD.Cd.ResetStatus();
        this.goalPos = Util.Instance.RandomPos(transform.position, this.randomRange);
        this.isReachGoal = false;
    }

    protected virtual void CheckReachGoal()
    {
        if (Vector2.Distance(this.transform.position, this.goalPos) < 0.1f)
        {
            this.isReachGoal = true;
            this.moveRandomCD.GetRandomDuration();
        }
    }

    //===========================================Weapon===========================================
    protected virtual void WeaponHandling()
    {
        if (this.identifyTarget.Target == null || this.weapon == null) return;
        WeaponUtil.Instance.WeaponHandling(this.weapon, 1, 1, this, this);
    }

    protected virtual void WeaponHolding()
    {
        if (this.identifyTarget.Target == null || this.weapon == null) return;
        WeaponUtil.Instance.WeaponHolding(this.weapon, this.rightArm, transform.position, 
            this.identifyTarget.Target.position);
    }

    //========================================Hp Receiver=========================================
    public int GetCurrHp()
    {
        return this.hp;
    }

    public FactionType GetFactionType()
    {
        return this.faction;
    }

    public void ReceiveHp(int hp)
    {
        this.hp += hp;
        if (this.hp > this.maxHp) this.hp = this.maxHp;

        if (this.hp <= 0)
        {
            this.hp = 0;
            this.isDead = true;
        }
    }

    //=======================================Mana Receiver========================================
    public void ReceiveMana(int mana)
    {
        return;
    }

    public int GetCurrMana()
    {
        return 0;
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        CrepSO crepSO = (CrepSO)this.so;
        if (crepSO == null)
        {
            Debug.LogError("CrepSO is null", transform.gameObject);
            return;
        }

        this.moveRandomCD = new RandomCooldown(Time.fixedDeltaTime, 
            crepSO.MinMoveRandomCD, crepSO.MaxMoveRandomCD);
        this.randomRange = crepSO.RandomRange;
    }
}

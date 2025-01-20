using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crep : NPC, HpReceiver, ManaReceiver, WeaponUser
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
    [SerializeField] protected TempWeapon weapon;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rightArm, transform.Find("RightArm"), "LoadRightArm()");
        this.LoadChildComponent(ref this.weapon, transform.Find("Weapon"), "LoadWeapon()");
        this.LoadSO(ref this.so, "SO/Character/NPC/Crep/" + transform.name);

        // Weapon
        if (this.weapon != null) this.weapon.SetUser(this);

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

    //========================================Weapon User=========================================
    public bool CanUseSkill(TempSkill skill)
    {
        if (this.hp <= skill.HpCost) return false;
        else return true;
    }

    public int GetFirstSkillState()
    {
        if (this.identifyTarget.Target == null) return 0;
        else return 2;
    }

    public int GetSecondSkillState()
    {
        if (this.identifyTarget.Target == null) return 0;
        else return 2;
    }

    public void ConsumePower(TempSkill skill)
    {
        this.hp -= skill.HpCost;
    }

    //===========================================Weapon===========================================
    protected virtual void WeaponHolding()
    {
        if (this.identifyTarget.Target == null || this.weapon == null) return;
        WeaponUtil.Instance.WeaponHolding(this.weapon, this.rightArm, transform.position, 
            this.identifyTarget.Target.position);
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
        this.identifyTarget.Owner = transform;
        this.randomRange = crepSO.RandomRange;
    }
}

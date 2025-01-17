using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkill : Skill
{
    //==========================================Variable==========================================
    [Header("Shoot Skill")]
    [SerializeField] protected float forcePower;
    [SerializeField] protected float pushBackDuration;
    [SerializeField] protected bool isRecharging;

    //==========================================Get Set===========================================
    public bool IsRecharging { get => isRecharging; set => isRecharging = value; }

    //========================================Constructor=========================================
    public AttackSkill(int manaCost, int hpCost, Cooldown skillCD, 
        float forcePower, float pushBackDuration) :
        base(manaCost, hpCost, skillCD) 
    {
        this.forcePower = forcePower;
        this.pushBackDuration = pushBackDuration;
        this.isRecharging = false;
    }

    public AttackSkill(AttackSkillSO so, float waitTime) : 
        base(so.ManaCost, so.HpCost, new Cooldown(1 / so.AttackRate, waitTime))
    {
        this.forcePower = so.ForcePower;
        this.pushBackDuration = so.PushBackDuration;
        this.isRecharging = false;
    }

    //===========================================Method===========================================
    public virtual void Recharging() 
    {
        if (!this.isRecharging) return;
        this.skillCD.CoolingDown();
    }

    protected virtual void PushBack(PushBackReceiver pushBackrecv, 
        Vector2 mainObjPos, Vector2 targetPos)
    {
        Vector2 dir = (mainObjPos - targetPos).normalized;
        Vector2 pushBackForce = dir * this.forcePower;
        pushBackrecv.ReceiveForce(pushBackForce, this.pushBackDuration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : AttackSkill
{
    //==========================================Variable==========================================
    [Header("Melee")]
    [SerializeField] private Cooldown attackCD;
    [SerializeField] private int damage;
    [SerializeField] private bool isAttacking;

    //==========================================Get Set===========================================
    public Cooldown AttackCD { get => attackCD; set => attackCD = value; }
    public int Damage { get => damage; set => damage = value; }
    public float ForcePower { get => forcePower; set => forcePower = value; }
    public float PushBackDuration { get => pushBackDuration; set => pushBackDuration = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    //========================================Constructor=========================================
    public MeleeSkill(int manaCost, int hpCost, Cooldown skillCD, Cooldown attackCD, 
        int damage, float forcePower, float pushBackDuration) : 
        base(manaCost, hpCost, skillCD, forcePower, pushBackDuration)
    {
        this.attackCD = attackCD;
        this.damage = damage;
        this.isAttacking = false;
        this.isRecharging = false;
    }

    public MeleeSkill(MeleeSkillSO so, float waitTime) : base(so, waitTime)
    {
        this.attackCD = new Cooldown(so.AttackDuration, waitTime);
        this.damage = so.Damage;
        this.forcePower = so.ForcePower;
        this.pushBackDuration = so.PushBackDuration;
        this.isAttacking = false;
        this.isRecharging = false;
    }

    //===========================================Method===========================================
    public void Attacking(HpReceiver hpRecv, ManaReceiver manaRecv, Collider2D attackCollider)
    {
        if (!this.attackCD.IsReady
            || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;

        attackCollider.enabled = true;
        attackCollider.isTrigger = true;

        this.isAttacking = true;
        this.isRecharging = false;
        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
        this.attackCD.ResetStatus();
    }

    public void MeleeAttacking(Collider2D attackCollider)
    {
        if (!this.isAttacking) return;
        this.AttackRecharge();

        if (!this.attackCD.IsReady) return;
        this.FinishAttack(attackCollider);
    }

    public void OnCollide(HpReceiver hpRecv, PushBackReceiver pushBackRecv, 
        Transform owner,Transform collidedObj)
    {
        this.DealDamage(hpRecv);
        this.PushBack(pushBackRecv, owner.position, collidedObj.position);
    }

    private void AttackRecharge()
    {
        this.attackCD.CoolingDown();
    }

    private void FinishAttack(Collider2D attackCollider)
    {
        attackCollider.enabled = false;
        this.isAttacking = false;
        this.attackCD.ResetStatus();
    }

    private void DealDamage(HpReceiver receiver)
    {
        receiver.ReceiveHp(-this.damage);
    }
}

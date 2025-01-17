using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreSkill : AttackSkill
{
    //==========================================Variable==========================================
    [Header("Gore")]
    [SerializeField] private Cooldown goreCD;
    [SerializeField] private int damage;
    [SerializeField] private bool isGoring;

    //========================================Constructor=========================================
    public GoreSkill(int manaCost, int hpCost, Cooldown skillCD,
        float forcePower, float pushBackDuration, Cooldown goreCD, int damage) : 
        base(manaCost, hpCost, skillCD, forcePower, pushBackDuration) 
    { 
        this.goreCD = goreCD; 
        this.damage = damage;
        this.isGoring = false;
    }

    //===========================================Method===========================================
    public void Goring()
    {
        if (!this.isGoring) return;
        this.goreCD.CoolingDown();
    }

    public void FinishGore()
    {
        if (!this.goreCD.IsReady) return;
        this.isGoring = false;
        this.goreCD.ResetStatus();
        this.isRecharging = true;
    }

    public void ActivateGore()
    {
        if (!this.skillCD.IsReady) return;
        this.isRecharging = false;
        this.skillCD.ResetStatus();
    }

    public void OnCollide(HpReceiver hpRecv, PushBackReceiver pushBackRecv, Transform owner, Transform collidedObj)
    {
        hpRecv.ReceiveHp(-this.damage);
        this.OnCollide(hpRecv, pushBackRecv, owner, collidedObj);
    }
}

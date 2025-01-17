using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstGun : Weapon, IAttackable
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Normal Gun===")]
    [SerializeField] private BurstSkill burstSkill;

    //===========================================Unity============================================
    private void FixedUpdate()
    {
        this.RechargeSkill();
    }

    //========================================IAttackable=========================================
    public void Attack(HpReceiver hpRecv, ManaReceiver manaRecv, int state)
    {
        if (state <= 0) return;
        this.burstSkill.Shooting(hpRecv, manaRecv);
    }

    //========================================Burst Skill=========================================
    private void RechargeSkill()
    {
        this.burstSkill.Recharging();
    }

    private void DefaultSkill()
    {

    }
}

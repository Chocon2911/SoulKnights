using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RegenSkill : Skill
{
    //==========================================Variable==========================================
    [SerializeField] private Cooldown regenCD;
    [SerializeField] private int regenInt;
    [SerializeField] private float regenFloat;
    [SerializeField] private bool isRecharging;
    [SerializeField] private bool isRegening;

    //==========================================Get Set===========================================
    public Cooldown RegenCD { get => regenCD; set => regenCD = value; }
    public int RegenInt { get => regenInt; set => regenInt = value; }
    public float RegenFloat { get => regenFloat; set => regenFloat = value; }
    public bool IsRecharging { get => isRecharging; set => isRecharging = value; }
    public bool IsRegening { get => isRegening; set => isRegening = value; }

    //========================================Constructor=========================================
    public RegenSkill(int manaCost, int hpCost, Cooldown skillCD, Cooldown regenCD, 
        int regenInt, float regenFloat) : base(manaCost, hpCost, skillCD)
    {
        this.regenCD = regenCD;
        this.regenInt = regenInt;
        this.regenFloat = regenFloat;
        this.isRecharging = false;
        this.isRegening = false;
    }

    public RegenSkill(RegenSkillSO so, float waitTime) 
        : base(so.ManaCost, so.HpCost, new Cooldown(so.SkillRechargeTime, waitTime)) 
    { 
        this.regenCD = new Cooldown(so.RegenTime, waitTime);
        this.regenInt = so.RegenAmountInt;
        this.regenFloat = so.RegenAmountFloat;
        this.isRecharging = false;
        this.isRegening = false;
    }

    //===========================================Method===========================================
    public void Regenerating()
    {
        if (!this.isRegening) return;
        this.regenCD.CoolingDown();
    }

    public void Recharging()
    {
        if (!this.isRecharging) return;
        this.skillCD.CoolingDown();
    }

    public void StartRegen()
    {
        if (!this.skillCD.IsReady) return;
        this.isRecharging = false;
        this.isRegening = true;
        this.skillCD.ResetStatus();
    }

    public void Regen(HpReceiver hpRecv, ManaReceiver manaRecv, ref int myValue)
    {
        if (!this.CanRegen(hpRecv, manaRecv)) return;
        myValue += regenInt;
        this.FinishRegen(hpRecv, manaRecv);
    }

    public void Regen(HpReceiver hpRecv, ManaReceiver manaRecv, ref float myValue)
    {
        if (!this.CanRegen(hpRecv, manaRecv)) return; 
        myValue += regenFloat;
        this.FinishRegen(hpRecv, manaRecv);
    }

    public void FinishSkill()
    {
        this.SkillCD.ResetStatus();
        this.regenCD.ResetStatus();
        this.isRecharging = false;
        this.isRegening = false;
    }

    private void FinishRegen(HpReceiver hpRecv, ManaReceiver manaRecv)
    {
        this.regenCD.ResetStatus();
        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
    }

    private bool CanRegen(HpReceiver hpRecv, ManaReceiver manaRecv)
    {
        if (!this.regenCD.IsReady
            || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return false;
        return true;
    }
}

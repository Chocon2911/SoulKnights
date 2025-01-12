using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Skill
{
    //==========================================Variable==========================================
    [SerializeField] protected int manaCost;
    [SerializeField] protected int hpCost;
    [SerializeField] protected Cooldown skillCD;

    //==========================================Get Set===========================================
    public int ManaCost
    {
        get => this.manaCost;
        set => this.manaCost = value;
    }

    public int HpCost
    {
        get => this.hpCost;
        set => this.hpCost = value;
    }

    public Cooldown SkillCD
    {
        get => this.skillCD;
        set => this.skillCD = value;
    }

    //========================================Constructor=========================================
    public Skill(int manaCost, int hpCost, Cooldown skillCD)
    {
        this.manaCost = manaCost;
        this.hpCost = hpCost;
        this.skillCD = skillCD;
    }

    //===========================================Method===========================================
    public bool CanUseSkill(int hp, int mana)
    {
        if (mana < this.manaCost || hp <= this.hpCost) { Debug.Log("Hello"); return false; }
        return true;
    }
}

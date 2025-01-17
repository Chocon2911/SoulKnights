using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Skill
{
    //==========================================Variable==========================================
    [SerializeField] private int manaCost;
    [SerializeField] private int hpCost;
    [SerializeField] private Cooldown skillCD;

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
    public bool CanUseSkill(Player player)
    {
        if (player.Mana < this.manaCost || player.Hp <= this.hpCost) { Debug.Log("Hello"); return false; }
        return true;
    }

    //public abstract void UseSkill();
    //public abstract void MyFixedUpdate();
    //Public abstract void MyUpdate();
}

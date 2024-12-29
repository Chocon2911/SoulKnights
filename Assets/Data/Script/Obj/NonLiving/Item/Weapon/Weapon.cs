using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : BaseItem
{
    //==========================================Variable==========================================
    // Child Component
    [SerializeField] protected BaseSkill skill1;
    [SerializeField] protected BaseSkill skill2;

    //==========================================Skill_1===========================================
    public void PerformSkill1()
    {
        if (this.skill1 is ShootSkill)
        {
            ShootSkill tempSkill1 = (ShootSkill)this.skill1;
            tempSkill1.PerformSkill(this.transform.position, this.transform.rotation);
        }
    }

    //==========================================Skill_2===========================================
    public void PerformSkill2()
    {

    }
}

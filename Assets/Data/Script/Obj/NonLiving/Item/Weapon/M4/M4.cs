using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : BaseWeapon
{
    //==========================================Variable==========================================
    // Skill1
    [SerializeField] private float skill1Damage = 1;
    [SerializeField] private float skill1AtkSpeed = 10;
    [SerializeField] private float skill1ReloadTime = 1.5f;

    // Skill2
    [SerializeField] private float skill2Damage = 4;
    [SerializeField] private float skill2AtkSpeed = 1;

    //===========================================Unity============================================

    //===========================================Other============================================
    private void DefaultStat()
    {
        
    }
}

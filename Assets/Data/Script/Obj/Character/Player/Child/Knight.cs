using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Player
{
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Knight===")]
    [SerializeField] private DualWieldSkill dualWield;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Character/Player/Knight/Player");
        base.LoadComponents();
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
    }
}

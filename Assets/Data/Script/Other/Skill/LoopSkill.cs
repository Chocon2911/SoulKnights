using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSkill : BaseSkill
{
    [SerializeField] private int loopTimes;

    //===========================================Method===========================================
    private System.Action Perform;

    //==========================================Override==========================================
    public override void PerformSkill()
    {
        for (int i = 0; i < loopTimes; i++) this.Perform();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ShootSkill : BaseSkill
{
    //===========================================Method===========================================
    public virtual void PerformSkill(Vector3 spawnPos, Quaternion spawnRot)
    {
        if (!this.skillCD.IsReady) return;
        this.ActivateSkill(spawnPos, spawnRot);
    }

    //==========================================Abstract==========================================
    protected abstract void ActivateSkill(Vector3 spawnPos, Quaternion spawnRot);
}

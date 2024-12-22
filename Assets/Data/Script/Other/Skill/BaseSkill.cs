using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseSkill
{
    //==========================================Variable==========================================
    [SerializeField] protected CountDown skillCooldown;

    //===========================================Method===========================================
    public abstract void PerformSkill();
    protected abstract void ActivateSkill();
    protected abstract void DeactivateSkill();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RandomCooldown
{
    //==========================================Variable==========================================
    [SerializeField] private Cooldown cd;
    [SerializeField] private float maxDuration;
    [SerializeField] private float minDuration;

    //==========================================Get Set===========================================
    public Cooldown Cd { get => cd; set => cd = value; }
    public float MaxDuration { get => maxDuration; set => maxDuration = value; }
    public float MinDuration { get => minDuration; set => minDuration = value; }

    //========================================Constructor=========================================
    public RandomCooldown(float waitTime, float minDuration, float maxDuration)
    {
        this.cd = new Cooldown(0, waitTime);
        this.maxDuration = maxDuration;
        this.minDuration = minDuration;
    }

    //===========================================Method===========================================
    public void GetRandomDuration()
    {
        this.cd.TimeLimit = Util.Instance.RandomFloat(this.minDuration, this.maxDuration);
    }

    public void CoolingDown()
    {
        this.cd.CoolingDown();
    }
}

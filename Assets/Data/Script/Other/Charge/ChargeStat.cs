using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChargeStat
{
    //==========================================Variable==========================================
    [SerializeField] private Cooldown chargeCD;
    [SerializeField] private int maxGrade;
    [SerializeField] private int tempGrade;
    [SerializeField] private bool isCharging; 

    //==========================================Get Set===========================================
    public Cooldown ChargeCD { get => chargeCD; set => chargeCD = value; }
    public int MaxGrade { get => maxGrade; set => maxGrade = value; }
    public int TempGrade { get => tempGrade; set => tempGrade = value; }
    public bool IsCharging { get => isCharging; set => isCharging = value; }

    //========================================Constructor=========================================
    public ChargeStat(int maxGrade, int tempGrade, Cooldown chargeCD)
    {
        this.chargeCD = chargeCD;
        this.maxGrade = maxGrade;
        this.tempGrade = tempGrade;
    }

    //===========================================Method===========================================
    public void ActivateCharge()
    {
        this.isCharging = true;
    }

    public void Charging()
    {
        this.chargeCD.CoolingDown();

        if (!this.chargeCD.IsReady || this.tempGrade >= this.maxGrade) return;
        this.tempGrade++;
        this.chargeCD.ResetStatus();
    }

    public void FinishCharge()
    {
        this.isCharging = false;
        this.tempGrade = 0;
        this.chargeCD.ResetStatus();
    }

    public float GetTotalTime()
    {
        return this.chargeCD.TimeLimit * this.maxGrade;
    }
}

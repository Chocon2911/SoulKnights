using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    //==========================================Variable==========================================
    [Header("Cool Down")]
    // Stat
    [SerializeField] private float timeLimit;
    [SerializeField] private float timer;
    [SerializeField] private float waitTime;
    [SerializeField] private bool isReady;

    //==========================================Get Set===========================================
    public float TimeLimit
    {
        get => timeLimit;
        set => timeLimit = value;
    }
    
    public float Timer
    {
        get => timer;
    }

    public float WaitTime
    {
        get => waitTime;
        set => waitTime = value;
    }

    public bool IsReady
    {
        get => isReady;
    }

    //========================================Constructor=========================================
    public Cooldown(float timeLimit, float waitTime)
    {
        // Stat
        this.timeLimit = timeLimit;
        this.waitTime = waitTime;
        this.timer = 0;
    }

    //===========================================Method===========================================
    public void CoolingDown()
    {
        if (this.timer >= this.timeLimit)
        {
            isReady = true; 
            return;
        }
        
        this.OnCD();
        this.timer += this.waitTime;
    }

    public Action OnCD = () => { };

    public void ResetStatus()
    {
        this.timer = 0;
        this.isReady = false;
    }
}

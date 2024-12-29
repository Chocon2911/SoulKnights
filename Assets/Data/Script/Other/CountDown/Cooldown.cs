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

    //==========================================Get Set===========================================
    public float Timer
    {
        get => timer;
    }

    public bool IsReady
    {
        get => this.timer >= this.timeLimit;
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
    public void Counting()
    {
        if (this.timer >= this.timeLimit - this.waitTime) return;
        
        this.PerformWhileCounting();
        this.timer += this.waitTime;
    }

    public System.Action PerformWhileCounting = () => { };

    public void ResetTimer()
    {
        this.timer = 0;
    }
}

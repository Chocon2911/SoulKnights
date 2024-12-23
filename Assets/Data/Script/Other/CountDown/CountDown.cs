using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CountDown
{
    //==========================================Variable==========================================
    [Header("Count Down")]
    [SerializeField] private float timeLimit;
    [SerializeField] private float timer;
    [SerializeField] private bool isReady;
    private float waitTime = 0.01f;

    //==========================================Get Set===========================================
    public float Timer
    {
        get => timer;
    }
    
    public bool IsReady
    {
        get => isReady;
    }

    //========================================Constructor=========================================
    public CountDown(float timeLimit)
    {
        this.timeLimit = timeLimit;
        this.timer = 0;
        this.isReady = false;
    }

    //===========================================Method===========================================
    public IEnumerator Counting()
    {
        while (true)
        {
            if (this.timer >= this.timeLimit - this.waitTime)
            {
                this.isReady = true;
                yield break;
            }

            this.timer += this.waitTime;
            yield return new WaitForSeconds(this.waitTime);
        }
    }

    public void ResetTimer()
    {
        this.timer = 0;
        this.isReady = false;
    }
}

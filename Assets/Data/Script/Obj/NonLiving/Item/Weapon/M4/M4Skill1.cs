using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M4Skill1 : BaseSkill
{
    //==========================================Variable==========================================
    [Header("M4 Skill1")]
    // Stat
    [SerializeField] private bool atkIsReady;
    [SerializeField] private bool isReloaded;

    // Component
    [SerializeField] private CountDown atkCD;
    [SerializeField] private CountDown reloadCD;
    [SerializeField] private BulletObj bullet;

    //========================================Constructor=========================================
    public M4Skill1()
    {
        // Atk
        this.atkCD.PerformWhenCountDone = () => { this.atkIsReady = true; };
        this.atkCD.PerformWhileCounting = () => { this.atkIsReady = false; };

        // Reload
        this.reloadCD.PerformWhenCountDone = () => { this.isReloaded = true; };
        this.reloadCD.PerformWhenCountDone = () => { this.isReloaded = false; };
    }

    //==========================================Override==========================================
    public override void PerformSkill()
    {
        if (this.bullet == null || this.atkCD == null || this.reloadCD == null) return;
        else if (!this.atkIsReady || !this.isReloaded) return;

        // Spawn Bullet
    }

    //===========================================Other============================================
    private void DefaultStat()
    {

    }
}

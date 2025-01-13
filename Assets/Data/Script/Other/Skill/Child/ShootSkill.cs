using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShootSkill : Skill
{
    [SerializeField] private bool isCharging;
    public bool IsCharging { get => this.isCharging; set => this.isCharging = value; }

    //========================================Constructor=========================================
    public ShootSkill(int manaCost, int hpCost, Cooldown skillCD) : base(manaCost, hpCost, skillCD)
    {
        this.isCharging = false;
    }

    public ShootSkill(ShootSkillSO so, float waitTime) : 
        base(so.ManaCost, so.HpCost, new Cooldown(1 / so.FireRate, waitTime)) 
    {
        this.isCharging = false;
    }

    //===========================================Method===========================================
    public void Shooting(HpReceiver hpRecv, ManaReceiver manaRecv, Transform bulletObj, 
        Transform shooter, Vector3 bulletPos, Quaternion bulletRot)
    {
        if (!this.skillCD.IsReady 
            || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;

        Transform newBullet = SkillUtil.Instance.Shoot(bulletObj, bulletPos, bulletRot);
        if (newBullet == null) return;

        Bullet bullet = newBullet.GetComponent<Bullet>();
        if (bullet == null)
        {
            Debug.LogError("Bullet is null", shooter.gameObject);
            return;
        }

        bullet.SetShooter(shooter);
        newBullet.gameObject.SetActive(true);
        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
        this.skillCD.ResetStatus();
    }

    public void Recharging()
    {
        if (!this.isCharging) return;
        this.skillCD.CoolingDown();
    }
}

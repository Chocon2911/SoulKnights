using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : Skill
{
    //========================================Constructor=========================================
    public ShootSkill(int manaCost, int hpCost, Cooldown skillCD) : base(manaCost, hpCost, skillCD)
    {

    }

    //===========================================Method===========================================
    public void Shooting(HpReceiver hpRecv, ManaReceiver manaRecv, Transform bulletObj, Transform shooter, Vector3 bulletPos, Quaternion bulletRot)
    {
        if (!this.skillCD.IsReady) return;

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
        this.skillCD.CoolingDown();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleShotSkill : AttackSkill
{
    //========================================Constructor=========================================
    public SingleShotSkill(int manaCost, int hpCost, Cooldown skillCD, 
        float forcePower, float pushBackDuration) : 
        base(manaCost, hpCost, skillCD, forcePower, pushBackDuration)
    { }

    public SingleShotSkill(SingleShotSkillSO so, float waitTime) : 
        base(so, waitTime) 
    { }

    //===========================================Method===========================================
    public void Shooting(HpReceiver hpRecv, ManaReceiver manaRecv, Transform bulletObj, 
        Transform shooter, Vector3 bulletPos, float bulletAngle)
    {
        if (!this.skillCD.IsReady 
            || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;

        Quaternion bulletRot = Quaternion.Euler(0, 0, bulletAngle);
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
}

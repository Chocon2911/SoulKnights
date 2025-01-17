using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShotSkill : AttackSkill
{
    //==========================================Variable==========================================
    [Header("Charge Shot")]
    [SerializeField] private Transform tempBulletObj;

    //========================================Constructor=========================================
    public ChargeShotSkill(int manaCost, int hpCost, Cooldown skillCD, 
        float forcePower, float pushBackDuration) :
        base(manaCost, hpCost, skillCD, forcePower, pushBackDuration)
    {
        this.tempBulletObj = null;
    }

    //===========================================Method===========================================
    public void ActivateCharge(HpReceiver hpRecv, ManaReceiver manaRecv, 
        Transform bulletObj, Transform owner)
    {
        if (!this.skillCD.IsReady
            || !CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;
        
        Transform newBullet = SkillUtil.Instance.Shoot(bulletObj, owner.position, owner.rotation);
        if (newBullet == null) return;

        Bullet bullet = newBullet.GetComponent<Bullet>();
        if (bullet == null)
        {
            Debug.LogError("Bullet is null", owner.gameObject);
            return;
        }

        bullet.SetShooter(owner);
        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
    }

    public void Shoot(HpReceiver hpRecv, ManaReceiver manaRecv, ChargableBullet bullet)
    {
        if (this.tempBulletObj == null) return;
        bullet.FinishCharge();
        this.skillCD.ResetStatus();
    }
}

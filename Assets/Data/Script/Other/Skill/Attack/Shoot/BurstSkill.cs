using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstSkill : AttackSkill
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] private Cooldown burstCD;
    [SerializeField] private int burstCount;
    [SerializeField] private int tempBurstCount;
    [SerializeField] private bool isBursting;

    //==========================================Get Set===========================================
    public Cooldown BurstCD { get => burstCD; set => burstCD = value; }
    public int BurstCount { get => burstCount; set => burstCount = value; }

    //========================================Constructor=========================================
    public BurstSkill(int manaCost, int hpCost, Cooldown skillCD, Cooldown burstCD, int burstCount) : 
        base(manaCost, hpCost, skillCD)
    {
        this.burstCD = burstCD;
        this.burstCount = burstCount;
        this.isBursting = false;
    }

    public BurstSkill(BurstSkillSO so, float waitTime) : base(so, waitTime) 
    { 
        this.burstCD = new Cooldown(so.BurstInterval, waitTime);
        this.burstCount = so.BurstCount;
        this.tempBurstCount = this.burstCount;
        this.isBursting = false;
    }

    //===========================================Method===========================================
    public void Shooting(HpReceiver hpRecv, ManaReceiver manaRecv)
    {
        if (!this.skillCD.IsReady
            || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;

        this.isBursting = true;
        this.isRecharging = false;
        this.tempBurstCount = this.burstCount;
        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
        this.skillCD.ResetStatus();
    }

    public void Bursting(Transform bulletObj, Transform shooter, 
        Vector3 bulletPos, float bulletAngle)
    {
        this.RechargeBurst();
        this.DoShoot(bulletObj, shooter, bulletPos, bulletAngle);
        this.FinishBurst();
    }

    private void DoShoot(Transform bulletObj, Transform shooter,
        Vector3 bulletPos, float bulletAngle)
    {
        if (!this.burstCD.IsReady) return;
        this.tempBurstCount--;
        this.burstCD.ResetStatus();

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
    }

    private void FinishBurst()
    {
        if (this.tempBurstCount > 0) return;
        this.isBursting = false;
        this.isRecharging = true;
    }

    private void RechargeBurst()
    {
        if (!this.isBursting) return;
        this.burstCD.CoolingDown();
    }
}

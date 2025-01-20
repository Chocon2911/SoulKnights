using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBurstSkill : TempShootSkill
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] private Cooldown burstCD;
    [SerializeField] private int burstCount;
    [SerializeField] private int tempBurstCount;
    [SerializeField] private bool isBursting;

    //===========================================Method===========================================
    private void Bursting()
    {
        if (this.isBursting) this.RechargeBurst();
        if (this.burstCD.IsReady) this.DoShoot();
        if (this.tempBurstCount <= 0)this.FinishBurst();
    }

    private void DoShoot()
    {
        this.tempBurstCount--;
        this.burstCD.ResetStatus();

        Quaternion bulletRot = Quaternion.Euler(0, 0, this.user.GetShootAngle());
        Transform newBullet = SkillUtil.Instance.Shoot(bulletObj, this.user.GetBulletPos(), bulletRot);

        if (newBullet == null) return;
        Bullet bullet = newBullet.GetComponent<Bullet>();

        if (bullet == null)
        {
            Debug.LogError("Bullet is null", transform.gameObject);
            return;
        }

        bullet.SetShooter(this.owner);
        newBullet.gameObject.SetActive(true);
    }

    private void FinishBurst()
    {
        this.isBursting = false;
        this.tempBurstCount = this.burstCount;
        this.ResetSkillCD();
    }

    private void RechargeBurst()
    {
        this.burstCD.CoolingDown();
    }

    //==========================================Override==========================================
    public override void MyLoadComponents()
    {
        base.MyLoadComponents();
        this.LoadSO(ref this.so, "SO/Skill/Attack/Shoot/Burst/" + this.owner.name);
    }

    public override void MyFixedUpdate()
    {
        base.MyFixedUpdate();
        this.Bursting();
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        this.burstCD.ResetStatus();
        this.tempBurstCount = this.burstCount;
        this.isBursting = false;
    }

    public override void UseSkill()
    {
        this.isBursting = true;
        this.isRecharging = false;
        this.tempBurstCount = this.burstCount;
        this.user.ConsumePower(this);
        this.skillCD.ResetStatus();
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        BurstSkillSO burstSO = (BurstSkillSO)this.so;
        if (burstSO == null)
        {
            Debug.LogError("BurstSkillSO is null", transform.gameObject);
            return;
        }

        this.burstCount = burstSO.BurstCount;
        this.burstCD = new Cooldown(burstSO.BurstInterval, Time.fixedDeltaTime);
        this.tempBurstCount = this.burstCount;
    }
}

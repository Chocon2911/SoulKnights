using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSkill : AttackSkill
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;

    //==========================================Get Set===========================================
    public int BulletCount { get => bulletCount; set => bulletCount = value; }
    public float SpreadAngle { get => spreadAngle; set => spreadAngle = value; }

    //========================================Constructor=========================================
    public ShotgunSkill(int manaCost, int hpCost, Cooldown skillCD, 
        float forcePower, float pushBackDuration, int bulletCount, float spreadAngle)
        : base(manaCost, hpCost, skillCD, forcePower, pushBackDuration)
    {
        this.manaCost = manaCost;
        this.hpCost = hpCost;
        this.skillCD = skillCD;
        this.bulletCount = bulletCount;
        this.spreadAngle = spreadAngle;
    }

    public ShotgunSkill(ShotgunSkillSO so, float waitTime) : base(so, waitTime) 
    {
        this.bulletCount = so.BulletCount;
        this.spreadAngle = so.SpreadAngle;
    }

    //===========================================Method===========================================
    public void Shooting(HpReceiver hpRecv, ManaReceiver manaRecv, Transform bulletObj,
        Transform shooter, Vector3 bulletPos, float bulletAngle)
    {
        if (!this.skillCD.IsReady
            || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;

        List<Transform> newBullets = SkillUtil.Instance.Shotgun(bulletObj, this.bulletCount, bulletPos, bulletAngle, spreadAngle);
        if (newBullets == null)
        {
            Debug.LogError("ShotgunBullet List is null", shooter.gameObject);
            return;
        }

        foreach (Transform newBullet in newBullets)
        {
            if (newBullet == null) return;
            Bullet bullet = newBullet.GetComponent<Bullet>();

            if (bullet == null)
            {
                Debug.LogError("ShotgunBullet is null", shooter.gameObject);
                return;
            }

            bullet.SetShooter(shooter);
            newBullet.gameObject.SetActive(true);
        }

        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
        this.skillCD.ResetStatus();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShotgunSkill : TempShootSkill
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;

    //==========================================Override==========================================
    public override void MyLoadComponents()
    {
        base.MyLoadComponents();
        this.LoadSO(ref this.so, "SO/Skill/Attack/Shoot/Shotgun/" + this.owner.name);
    }

    public override void UseSkill()
    {
        List<Transform> newBullets = SkillUtil.Instance.Shotgun(bulletObj, this.bulletCount, 
            this.user.GetBulletPos(), this.user.GetShootAngle(), spreadAngle);

        if (newBullets == null)
        {
            Debug.LogError("ShotgunBullet List is null", transform.gameObject);
            return;
        }

        foreach (Transform newBullet in newBullets)
        {
            if (newBullet == null) return;
            Bullet bullet = newBullet.GetComponent<Bullet>();

            if (bullet == null)
            {
                Debug.LogError("ShotgunBullet is null", transform.gameObject);
                return;
            }

            bullet.SetShooter(this.owner);
            newBullet.gameObject.SetActive(true);
        }

        this.user.ConsumePower(this);
        this.ResetSkillCD();
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        ShotgunSkillSO shotgunSO = (ShotgunSkillSO)this.so;
        if (shotgunSO == null)
        {
            Debug.LogError("ShotgunSkillSO is null", transform.gameObject);
            return;
        }

        this.bulletCount = shotgunSO.BulletCount;
        this.spreadAngle = shotgunSO.SpreadAngle;
    }
}

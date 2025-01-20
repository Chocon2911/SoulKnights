using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSingleShotSkill : TempShootSkill
{
    //==========================================Override==========================================
    public override void MyLoadComponents()
    {
        base.MyLoadComponents();
        this.LoadSO(ref this.so, "SO/Skill/Attack/Shoot/SingleShot/" + this.owner.name);
    }

    public override void UseSkill()
    {    
        Quaternion bulletRot = Quaternion.Euler(0, 0, this.user.GetShootAngle());
        Transform newBullet = SkillUtil.Instance.Shoot(this.bulletObj, this.user.GetBulletPos(), bulletRot);
        if (newBullet == null) return;

        Bullet bullet = newBullet.GetComponent<Bullet>();
        if (bullet == null)
        {
            Debug.LogError("Bullet is null", transform.gameObject);
            return;
        }

        bullet.SetShooter(this.owner);
        newBullet.gameObject.SetActive(true);
        this.user.ConsumePower(this);
        this.ResetSkillCD();
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        SingleShotSkillSO singleShotSO = (SingleShotSkillSO)this.so;
        if (singleShotSO == null)
        {
            Debug.LogError("SingleShotSkillSO is null", transform.gameObject);
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TempAdvanceShotgunSkill : TempShootSkill
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private List<float> spreadAngles;

    //==========================================Override==========================================
    public override void ResetSkill()
    {
        this.ResetSkillCD();
    }

    public override void UseSkill()
    {
        for (int i = 0; i < this.spreadAngles.Count; i++)
        {
            Quaternion bulletRot = Quaternion.Euler(0, 0, this.user.GetShootAngle() + this.spreadAngles[i]);
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
        }

        this.user.ConsumePower(this);
        this.ResetSkillCD();
    }
}

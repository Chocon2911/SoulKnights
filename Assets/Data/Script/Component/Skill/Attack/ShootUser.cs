using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShootUser : SkillUser
{
    int GetSkillOrder(TempShootSkill skill);
    List<bool> CanShoot();
    float GetShootAngle();
    Vector3 GetBulletPos();
}

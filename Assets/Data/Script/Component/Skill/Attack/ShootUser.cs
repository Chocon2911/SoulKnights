using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShootUser : SkillUser
{
    List<bool> CanShoot();
    float GetShootAngle();
    Vector3 GetBulletPos();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DualWieldUser : SkillUser
{
    TempWeapon GetMainWeapon();
    bool CanUseDualWield();
    Vector2 GetOwnerPos();
    Vector2 GetTargetPos();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DualWieldUser : SkillUser
{
    Weapon GetMainWeapon();
    bool CanUseDualWield();
    int GetLeftClickState();
    int GetRightClickState();
    Vector2 GetOwnerPos();
}

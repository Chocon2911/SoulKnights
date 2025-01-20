using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponUser
{
    bool CanUseSkill(TempSkill skill);
    int GetFirstSkillState();
    int GetSecondSkillState();
    void ConsumePower(TempSkill skill);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillUser
{
    bool CanUseSkill(TempSkill skill);
    void ConsumePower(TempSkill skill);
}

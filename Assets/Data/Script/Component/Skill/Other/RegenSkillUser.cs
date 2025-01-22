using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RegenSkillUser : SkillUser
{
    RegenType GetRegenType();
    bool CanRegen();
    int maxInt();
    float maxFloat();
    ref int currInt();
    ref float currFloat();
}

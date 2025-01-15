using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUtil
{
    //==========================================Variable==========================================
    private static EffectUtil instance;
    public const float PoisonDealDamageDelay = 1f;
    public const float FireDealDamageDelay = 0.5f;

    //==========================================Get Set===========================================
    public static EffectUtil Instance
    {
        get
        {
            if (instance == null) instance = new EffectUtil();
            return instance;
        }
    }

    //========================================Constructor=========================================
    public EffectUtil()
    {
        if (instance != null) Debug.LogError("EffectUtil is already exist");
    }

    //===========================================Method===========================================
}

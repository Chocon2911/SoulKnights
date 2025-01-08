using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUtil
{
    //==========================================Variable==========================================
    private static EffectUtil instance;

    //==========================================Get Set===========================================
    public static EffectUtil Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EffectUtil();
            }

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

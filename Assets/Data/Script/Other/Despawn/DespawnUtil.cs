using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnUtil
{
    //==========================================Variable==========================================
    [Header("Despawn Util")]
    private static DespawnUtil instance;

    //==========================================Get Set===========================================
    public static DespawnUtil Instance
    {
        get
        {
            if (instance == null) instance = new DespawnUtil();
            return instance;
        }
    }

    //========================================Constructor=========================================
    public DespawnUtil()
    {
        if (instance != null) Debug.LogError("One DespawnUtil Only");
    }

    //===========================================Method===========================================
    public void Despawn(Transform despawnObj, Spawner spawner)
    {
        spawner.Despawn(despawnObj);
    }

    public void DespawnByTime(Cooldown despawnCD, Transform despawnObj, Spawner spawner)
    {
        despawnCD.CoolingDown();
        if (despawnCD.IsReady) this.Despawn(despawnObj, spawner);
    }

    public void DespawnByDistance(Vector2 mainPos, Vector2 targetPos, Transform despawnObj, Spawner spawner)
    {
        if (mainPos.x < targetPos.x && mainPos.y < targetPos.y) return;
        this.Despawn(despawnObj, spawner);
    }
}

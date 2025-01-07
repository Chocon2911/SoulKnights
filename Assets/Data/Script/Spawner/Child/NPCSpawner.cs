using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : Spawner
{
    //==========================================Variable==========================================
    private static NPCSpawner instance;
    public static NPCSpawner Instance => instance;

    //===========================================Unity============================================
    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("One Instance only", transform.gameObject);
            return;
        }

        instance = this;
        base.Awake();
    }
}

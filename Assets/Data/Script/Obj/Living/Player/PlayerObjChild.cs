using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerObjChild : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Player Obj Child")]
    [SerializeField] private PlayerObjManager manager;

    //==========================================Get Set===========================================
    public PlayerObjManager Manager => manager;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.manager, transform.parent, "LoadPlayer()");
    }

    //===========================================Other============================================
    public abstract void DefaultStat();
}

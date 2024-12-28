using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerComponent : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected PlayerObj player;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.player, transform.parent, "LoadPlayer()");
    }

    //==========================================Abstract==========================================
    public virtual void DefaultStat()
    {
        if (this.player == null)
        {
            Debug.LogError("Player is null", transform.gameObject);
            return;
        }
    }
}

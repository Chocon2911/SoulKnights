using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : BaseCharacter
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===NPC===")]
    [Header("Stat")]
    [SerializeField] protected FactionType faction;
    [SerializeField] protected bool isDead;

    [Header("Identify Target")]
    [SerializeField] protected IdentifyObjByCollide identifyTarget;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.identifyTarget, transform.Find("IdentifyTarget"), "LoadIdentifyTarget()");
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        NPCSO npcSO = (NPCSO)this.so;
        if (npcSO == null)
        {
            Debug.LogError("NPCSO is null", transform.gameObject);
            return;
        }

        this.faction = npcSO.Faction;
        this.identifyTarget.DefaultStat(npcSO.IdentifyObjByCollideSO);
    }
}

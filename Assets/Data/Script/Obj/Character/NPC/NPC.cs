using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    [Header("Identify Target")]
    [SerializeField] protected IdentifyObjByCollide identifyTarget;

    //==========================================Get Set===========================================

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.identifyTarget, transform.Find("IdentifyTarget"),
            "LoadIdentifyTarget()");
    }

    //========================================Hp Receiver=========================================
    public override FactionType GetFactionType()
    {
        return this.faction;
    }
    
    public override int GetCurrHp() 
    {
        return this.hp;
    }

    public override void ReceiveHp(int hp)
    {
        // Add Hp
        // Equals MaxHp if > MaxHp
        // Despawn if <= 0 
    }

    //======================================Identify Target=======================================
    private void 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : BaseCharacter
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

    //======================================Damage Receiver=======================================
    public override FactionType GetFactionType()
    {
        return this.faction;
    }

    //========================================Hp Receiver=========================================
    public override int GetCurrHp()
    {
        return this.hp;
    }

    public override void ReceiveHp(int hp)
    {
        this.hp += hp;
        if (this.hp > this.maxHp) this.hp = this.maxHp;
        if (this.hp <= 0)
        {
            this.hp = 0;
            this.isDead = true;
        }
    }

    //=======================================Mana Receiver========================================
    public override int GetCurrMana() { return 99999999; }

    public override void ReceiveMana(int mana) { }

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

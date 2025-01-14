using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseCharacter : BaseObj, HpReceiver
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Character===")]
    [Header("// Stat")]
    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;

    [Header("// Component")]
    [SerializeField] protected CapsuleCollider2D bodyCollider;
    [SerializeField] protected Rigidbody2D rb;

    //==========================================Get Set===========================================
    // Stat
    public int MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    public int Hp
    {
        get => hp;
    }

    // Component
    public CapsuleCollider2D BodyCollider
    {
        get => bodyCollider; 
        set => bodyCollider = value;
    }

    public Rigidbody2D Rb
    {
        get => rb;
        set => rb = value;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
    }

    //=========================================HpReceiver=========================================
    public abstract FactionType GetFactionType();
    public abstract int GetCurrHp();
    public abstract void ReceiveHp(int hp);


    //===========================================Other============================================
    protected virtual void DefaultCharacterStat(CharacterSO characterSO)
    {
        this.maxHp = characterSO.MaxHp;
    }

    protected virtual void DefaultCharacterComponent() 
    {
        this.rb.isKinematic = false;
        this.rb.gravityScale = 0;
        this.bodyCollider.isTrigger = false;
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        CharacterSO characterSO = (CharacterSO)this.so;
        if (characterSO == null)
        {
            Debug.LogError("CharacterSO is null", transform.gameObject);
            return;
        }

        this.DefaultCharacterStat(characterSO);
        this.DefaultCharacterComponent();
    }
}

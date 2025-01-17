using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : BaseCharacter, HpReceiver, ManaReceiver, PoisonEffReceiver, FireEffReceiver
{
    protected enum PlayerState
    {
        IDLE,
        MOVE,
        DASH
    }

    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Player===")]
    [Header("// Stat")]
    [SerializeField] protected int maxMana;
    [SerializeField] protected int mana;
    [SerializeField] protected int maxAmor;
    [SerializeField] protected int amor;
    [SerializeField] protected StatEffect poisonEff;
    [SerializeField] protected StatEffect fireEff;
    [SerializeField] protected FactionType factionType;
    [SerializeField] protected PlayerState playerState;

    [Header("// Dash Skill")]
    [SerializeField] protected DashSkill dashSkillNew;

    [Header("// Weapon")]
    [SerializeField] protected Transform rightArm;
    [SerializeField] protected List<Weapon> weapons;
    [SerializeField] protected int maxWeaponSlot;
    [SerializeField] protected int currWeaponSlot;

    [Header("// Amor Regen")]
    [SerializeField] protected RegenSkill amorRegenSkill;

    //==========================================Get Set===========================================
    // Stat
    public int MaxMana => maxMana;
    public int Mana => mana;
    public int MaxAmor => maxAmor;
    public int Amor => amor;
    public FactionType FactionType => FactionType;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rightArm, transform.Find("RightArm"), "LoadRightArm()");
        this.LoadComponent(ref this.weapons, transform.Find("Weapon"), "LoadWeapons()");
        // Default
        this.DefaultStat();
    }

    protected virtual void OnEnable()
    {
        this.Revive();
        this.canMove = true;
        this.dashSkillNew.IsRechargingSkill = true;
        this.currWeaponSlot = 1;
    }

    protected virtual void Update()
    {
        this.CheckDashUpdateNew();
    }

    protected virtual void FixedUpdate()
    {
        this.PoisonEffHandling();
        this.FireEffHandling();
        this.Moving();
        this.CheckDashFixedUpdateNew();
        this.WeaponHolding();
        this.WeaponHandling();
        this.AmorRegenFixedUpdate();
        this.AnimationHandling();
    }



    //============================================================================================
    //============================================Stat============================================
    //============================================================================================
    protected virtual void PoisonEffHandling()
    {
        this.poisonEff.CoolingDown();
        this.poisonEff.DealingDamage(ref this.hp);
    }

    protected virtual void FireEffHandling()
    {
        this.fireEff.CoolingDown();
        this.poisonEff.DealingDamage(ref this.hp);
    }



    //============================================================================================
    //==========================================Movement==========================================
    //============================================================================================
    protected void Moving()
    {
        this.rb.velocity = Vector2.zero;
        this.playerState = PlayerState.IDLE;
        this.dashSkillNew.Dashing(this.rb);
        if (this.canMove && !this.dashSkillNew.IsUsingSkill) this.MoveByKeyboard();
    }

    protected virtual void MoveByKeyboard()
    {
        Vector2 moveDir = InputManager.Instance.MoveDir.normalized;
        
        if (moveDir == Vector2.zero) return;
        this.playerState = PlayerState.MOVE;

        if (moveDir.x < 0) this.image.flipX = true;
        else this.image.flipX = false;
        MovementUtil.Instance.MoveByKeyboard(this.rb, this.moveSpeed);
    }



    //============================================================================================
    //=========================================Animation==========================================
    //============================================================================================
    protected virtual void AnimationHandling()
    {
        if (this.playerState == PlayerState.IDLE) this.myAnimator.SetInteger("State", 0);
        else if (this.playerState == PlayerState.MOVE) this.myAnimator.SetInteger("State", 1);
        else if (this.playerState == PlayerState.DASH) this.myAnimator.SetInteger("State", 2);
    }



    //============================================================================================
    //===========================================Skill============================================
    //============================================================================================

    //============================================Dash============================================

    protected virtual void CheckDashUpdateNew()
    {
        if (InputManager.Instance.SpaceState == 1) this.dashSkillNew.UseDash(this, this, InputManager.Instance.MoveDir);
    }

    protected virtual void CheckDashFixedUpdateNew()
    {
        this.dashSkillNew.DashRecharging();
        this.dashSkillNew.FinishDash();
    }

    protected virtual void DefaultDashSkillNew(PlayerSO playerSO)
    {
        this.dashSkillNew = new DashSkill(playerSO.DashSkillSO, Time.fixedDeltaTime);
        this.dashSkillNew.IsRechargingSkill = true;
        this.dashSkillNew.IsUsingSkill = false;
    }

    //=========================================Amor Regen=========================================
    protected virtual void AmorRegenFixedUpdate()
    {
        if (this.amor >= this.maxAmor) return;
        if (!this.amorRegenSkill.IsRegening) this.amorRegenSkill.IsRecharging = true;
        this.amorRegenSkill.Recharging();
        this.amorRegenSkill.StartRegen();
        this.amorRegenSkill.Regenerating();
        this.amorRegenSkill.Regen(this, this, ref this.amor);

        if (this.amor < this.maxAmor) return;
        this.amorRegenSkill.FinishSkill();
    }

    protected virtual void DefaultAmorRegenSkill(PlayerSO playerSO)
    {
        this.amorRegenSkill = new RegenSkill(playerSO.AmorRegenSO, Time.fixedDeltaTime);
        this.amorRegenSkill.IsRecharging = true;
        this.amorRegenSkill.IsRegening = false;
    }



    //============================================================================================
    //===========================================Weapon===========================================
    //============================================================================================
    protected void PickUpWeapon(Weapon weapon)
    {
        this.weapons.Add(weapon);
        this.maxWeaponSlot++;
    }

    protected virtual void WeaponHolding()
    {
        if (this.weapons.Count < this.currWeaponSlot 
            || this.weapons[this.currWeaponSlot - 1] == null) return;

        WeaponUtil.Instance.WeaponHolding(this.weapons[this.currWeaponSlot - 1], 
            this.rightArm, transform.position, InputManager.Instance.MousePos);
    }

    protected virtual void WeaponHandling()
    {
        if (this.weapons[this.currWeaponSlot - 1] == null) return;
        WeaponUtil.Instance.WeaponHandling(this.weapons[this.currWeaponSlot - 1], 
            InputManager.Instance.LeftClickState, InputManager.Instance.RightClickState,
            this, this);
    }



    //============================================================================================
    //============================================Stat============================================
    //============================================================================================

    //======================================Damage Receiver=======================================
    public override FactionType GetFactionType()
    {
        return this.factionType;
    }

    //========================================Hp Receiver=========================================
    public override int GetCurrHp()
    {
        return this.hp;
    }

    public override void ReceiveHp(int hp)
    {
        if (this.hp + hp > this.maxHp) this.hp = this.maxHp;
        else this.hp += hp;

        if (this.hp <= 0)
        {
            this.hp = 0;
            DespawnUtil.Instance.Despawn(transform, PlayerSpawner.Instance);
        }
    }

    //=======================================Mana Receiver========================================
    public override int GetCurrMana()
    {
        return this.mana;
    }

    public override void ReceiveMana(int mana)
    {
        if (this.mana + mana > this.maxMana) this.mana = this.maxMana;
        else this.mana += mana;
    }

    //======================================Poison Receiver=======================================
    void PoisonEffReceiver.ReceivePoisonEff(float poisonDuration, int damage)
    {
        this.poisonEff.ActivateEff(poisonDuration, damage);
    }

    //=======================================Fire Receiver========================================
    void FireEffReceiver.ReceiveFireEff(float fireDuration, int damage)
    {
        this.fireEff.ActivateEff(fireDuration, damage);
    }



    //============================================================================================
    //===========================================Other============================================
    //============================================================================================
    protected virtual void Revive()
    {
        this.hp = this.maxHp;
        this.mana = this.maxMana;
        this.amor = this.maxAmor;
    }



    //============================================================================================
    //==========================================Default===========================================
    //============================================================================================
    protected virtual void DefaultPlayerStat(PlayerSO playerSO)
    {
        this.maxMana = playerSO.MaxMana;
        this.mana = this.maxMana;
        this.maxAmor = playerSO.MaxAmor;
        this.amor = this.maxAmor;
        this.poisonEff = new StatEffect(0, EffectUtil.PoisonDealDamageDelay, 0);
        this.fireEff = new StatEffect(0, EffectUtil.FireDealDamageDelay, 0);
        this.factionType = FactionType.PLAYER;
    }

    protected virtual void DefaultWeapon(PlayerSO playerSO)
    {
        this.maxWeaponSlot = playerSO.MaxWeaponSlot;
    }



    //============================================================================================
    //==========================================Override==========================================
    //============================================================================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        PlayerSO playerSO = (PlayerSO)this.so;
        if (playerSO == null)
        {
            Debug.LogError("PlayerSO is null", transform.gameObject);
            return;
        }

        this.DefaultPlayerStat(playerSO);
        this.DefaultDashSkillNew(playerSO);
        this.DefaultWeapon(playerSO);
        this.DefaultAmorRegenSkill(playerSO);
    }
}

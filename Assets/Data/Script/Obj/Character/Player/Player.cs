using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : BaseCharacter, ManaReceiver, PoisonEffReceiver, FireEffReceiver
{
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
    [SerializeField] protected DamagableType factionType;

<<<<<<< Updated upstream
    [Header("// Dash Skill")]    
    [SerializeField] protected Skill dashSkill;
    [SerializeField] protected Cooldown dashCD;
    [SerializeField] protected Vector2 dashDir;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected bool canDash;
    [SerializeField] protected bool canDashCD;
=======
    [Header("// Movement")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool canMove;

    [Header("// Dash Skill")]
    [SerializeField] protected DashSkill dashSkillNew;
>>>>>>> Stashed changes

    [Header("// Weapon")]
    [SerializeField] protected List<Weapon> weapons;
    [SerializeField] protected int maxWeaponSlot;
    [SerializeField] protected int currWeaponSlot;

    [Header("// Amor Regen")]
    [SerializeField] protected Cooldown amorRegenCD;
    [SerializeField] protected bool canRegenAmor;

    //==========================================Get Set===========================================
    // Stat
    public int MaxMana => maxMana;

    public int Mana => mana;

    public int MaxAmor => maxAmor;

    public int Amor => amor;
    public DamagableType FactionType => FactionType;



    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.weapons, transform.Find("Weapon"), "LoadWeapons()");
        // Default
        this.DefaultStat();
    }

    protected virtual void Start()
    {
        this.Revive();
    }

    protected virtual void Update()
    {
        this.DashUpdate();
    }

    protected virtual void FixedUpdate()
    {
        this.Moving();
        this.DashFUpdate();
        this.WeaponHolding();
        this.WeaponHandling();
        this.AmorRegenerating();
    }



    //============================================================================================
    //==========================================Movement==========================================
    //============================================================================================
    protected void Moving()
    {
        if (!this.canMove || !this.canDash) return;
        MovementUtil.Instance.MoveByKeyboard(this.rb, this.moveSpeed);
    }

    protected virtual void DefaultMovement(PlayerSO playerSO)
    {
        this.moveSpeed = playerSO.MoveSpeed;
        this.canMove = true;
    }



    //============================================================================================
    //===========================================Skill============================================
    //============================================================================================

    //============================================Dash============================================
    //===Handling===
    protected virtual void DashFUpdate()
    {
        // Do Dash
        if (InputManager.Instance.ShiftState == 2 && this.dashSkill.SkillCD.IsReady) this.ActivateDash();
    }

    protected virtual void DashUpdate()
    {
        // On Dashing
        if (this.canDash) this.Dashing();

        // On Skill Cooling down
        else if (this.canDashCD) this.DashRecharging();

        // Dash Finish
        else if (this.dashCD.IsReady) this.ActivateDashRecharge();
    }

    //===Performing===
    protected virtual void ActivateDash()
    {
        this.dashDir = InputManager.Instance.MoveDir;
        this.canDash = true;
        this.canDashCD = false;
        this.dashSkill.SkillCD.ResetStatus();
    }

    protected virtual void ActivateDashRecharge()
    {
        this.canDash = false;
        this.canDashCD = true;
        this.dashCD.ResetStatus();
    }

    protected virtual void Dashing()
    {
        this.dashCD.CoolingDown();
    }

    protected virtual void DashRecharging()
    {
        this.dashSkill.SkillCD.CoolingDown();
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
        if (this.weapons.Count < this.currWeaponSlot || this.weapons[this.currWeaponSlot - 1] == null) return;

        Vector2 distance = InputManager.Instance.MousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        this.weapons[this.currWeaponSlot - 1].HoldingWeapon(transform, angle);
    }

    protected virtual void WeaponHandling()
    {
        if (this.weapons[this.currWeaponSlot - 1] == null) return;

        if (this.weapons[this.currWeaponSlot - 1] is IAttackable)
        {
            IAttackable firstAtk = (IAttackable)this.weapons[this.currWeaponSlot - 1];
            firstAtk.Attack(this, InputManager.Instance.LeftClickState);
        }

        if (this.weapons[this.currWeaponSlot - 1] is ISecondaryAttack)
        {
            ISecondaryAttack secondaryAtk = (ISecondaryAttack)this.weapons[this.currWeaponSlot - 1];
            secondaryAtk.SecondaryAttack(this, InputManager.Instance.RightClickState);
        }
    }



    //============================================================================================
    //============================================Stat============================================
    //============================================================================================

    //======================================Damage Receiver=======================================
<<<<<<< Updated upstream
    DamagableType DamageReceiver.GetFactionType()
=======
    public override FactionType GetFactionType()
>>>>>>> Stashed changes
    {
        return this.factionType;
    }
    //========================================Hp Receiver=========================================
<<<<<<< Updated upstream
    int HpReceiver.GetCurrHp()
=======
    public override int GetCurrHp()
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    int ManaReceiver.GetCurrMana()
=======
    public int GetCurrMana()
>>>>>>> Stashed changes
    {
        return this.mana;
    }

<<<<<<< Updated upstream
    public void Receive(int mana)
=======
    public void ReceiveMana(int mana)
>>>>>>> Stashed changes
    {
        if (this.mana + mana > this.maxMana) this.mana = this.maxMana;
        else this.mana += mana;
    }

    //======================================Poison Receiver=======================================
    public void ReceivePoisonEff(float poisonDuration, float damage)
    {
        throw new System.NotImplementedException();
    }

    //=======================================Fire Receiver========================================
    public void ReceiveFireEff(float fireDuration, float damage)
    {
        throw new System.NotImplementedException();
    }

    //============================================Amor============================================
    protected virtual void AmorRegenerating()
    {
        if (this.amor <= this.maxAmor) this.canRegenAmor = true;
        if (!this.canRegenAmor) return;

        this.amorRegenCD.CoolingDown();
        if (this.amorRegenCD.IsReady) this.amor++;
        if (this.amor >= this.maxAmor)
        {
            this.amor = this.maxAmor;
            this.canRegenAmor = false;
        }
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

    protected virtual void DefaultPlayerStat(PlayerSO playerSO)
    {
        this.maxMana = playerSO.MaxMana;
        this.maxAmor = playerSO.MaxAmor;
        this.factionType = DamagableType.PLAYER;
    }

    protected virtual void DefaultDashSkill(PlayerSO playerSO)
    {
        this.canDash = false;
        this.canDashCD = true;
        this.dashSpeed = playerSO.DashSpeed;

        Cooldown dashSkillCD = new Cooldown(playerSO.DashTime, Time.fixedDeltaTime);
        this.dashSkill = new Skill(playerSO.DashSkillMC, playerSO.DashSkillHC, dashSkillCD);

        this.dashCD.OnCD = () =>
        {
            MovementUtil.Instance.Move(this.rb, dashSpeed, this.dashDir);
        };
    }

    protected virtual void DefaultWeapon(PlayerSO playerSO)
    {
        this.maxWeaponSlot = playerSO.MaxWeaponSlot;
    }

    protected virtual void DefaultAmorRegen(PlayerSO playerSO)
    {
        this.amorRegenCD = new Cooldown(playerSO.AmorRegenTime, Time.fixedDeltaTime);
        this.canRegenAmor = false;
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
        this.DefaultDashSkill(playerSO);
        this.DefaultWeapon(playerSO);
        this.DefaultAmorRegen(playerSO);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    //==========================================Variable==========================================
    [Header("Player")]
    // Stat
    [SerializeField] protected int maxMana;
    [SerializeField] protected int mana;
    [SerializeField] protected int maxAmor;
    [SerializeField] protected int amor;
    [SerializeField] protected float dashTime;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected int maxWeaponSlot;
    [SerializeField] protected int currWeaponSlot;
    [SerializeField] protected float amorRegenRate;

    // Dash Skill    
    [SerializeField] protected Skill dashSkill;
    [SerializeField] protected Cooldown dashCD;
    [SerializeField] protected Vector2 dashDir;
    [SerializeField] protected bool canDash;
    [SerializeField] protected bool canDashCD;

    // Weapon
    [SerializeField] protected List<Weapon> weapons;

    // Amor
    [SerializeField] protected Cooldown amorRegenCD;
    [SerializeField] protected bool canRegenAmor;

    // Component
    [SerializeField] protected PlayerSO so;
    [SerializeField] protected HpReceiver hpRecv;
    [SerializeField] protected ManaReceiver manaRecv;

    //==========================================Get Set===========================================
    // Stat
    public int MaxMana => maxMana;

    public int Mana => mana;

    public int MaxAmor => maxAmor;

    public int Amor => amor;

    // Component
    public HpReceiver HpRecv => hpRecv;
    public ManaReceiver ManaRecv => manaRecv;



    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Load Component
        this.LoadComponent(ref this.hpRecv, transform.Find("Stat"), "LoadHpRecv()");
        this.LoadComponent(ref this.manaRecv, transform.Find("Stat"), "LoadManaRecv()");

        // Default
        this.DefaultStat();
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
        this.AmorRegenerating();
    }



    //============================================================================================
    //==========================================Movement==========================================
    //============================================================================================
    protected void Moving()
    {
        if (this.canDash) return;
        MovementUtil.Instance.MoveByKeyboard(this.rb, this.moveSpeed);
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

    //===Start===
    protected virtual void DefaultDash()
    {
        this.canDash = false;
        this.canDashCD = true;

        this.dashCD = new Cooldown(this.dashTime, Time.captureDeltaTime);
        this.dashCD.OnCD = () =>
        {
            MovementUtil.Instance.Move(this.rb, dashSpeed, this.dashDir);
        };
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
        Vector2 distance = InputManager.Instance.MousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        this.weapons[this.currWeaponSlot - 1].HoldingWeapon(transform, angle);
    }

    protected virtual void WeaponHandling()
    {
        if (this.weapons[this.currWeaponSlot - 1] is IAttackable)
        {
            (this.weapons[this.currWeaponSlot - 1] as IAttackable).Attack(this, InputManager.Instance.LeftClickState);
        }

        if (this.weapons[this.currWeaponSlot - 1] is ISecondaryAttack)
        {
            (this.weapons[this.currWeaponSlot - 1] as ISecondaryAttack).SecondaryAttack(InputManager.Instance.RightClickState);
        }
    }



    //============================================================================================
    //============================================Stat============================================
    //============================================================================================

    //========================================Hp Receiver=========================================
    protected virtual void DefaultHpRecv()
    {
        this.hpRecv.OnReceive = () =>
        {
            if (this.hp + this.hpRecv.ReceivedHp > this.maxHp) this.hp = this.maxHp;
            else this.hp += this.hpRecv.ReceivedHp;

            if (this.hp <= 0)
            {
                this.hp = 0;
                DespawnUtil.Instance.Despawn(transform, PlayerSpawner.Instance);
            }
        };
    }

    //=======================================Mana Receiver========================================

    protected virtual void DefaultManaRecv()
    {
        this.manaRecv.OnReceive = () =>
        {
            if (this.mana + this.manaRecv.ReceivedMana > this.maxMana) this.mana = this.maxMana;
            else this.mana += this.manaRecv.ReceivedMana;
        };
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

    protected virtual void DefaultAmorRegen()
    {
        this.canRegenAmor = false;
        this.amorRegenCD.TimeLimit = this.amorRegenRate;
    }



    //============================================================================================
    //===========================================Other============================================
    //============================================================================================
    protected virtual void Revive()
    {
        this.hp = this.maxHp;
        this.mana = this.maxMana;
        this.amor = this.maxMana;
    }



    //============================================================================================
    //==========================================Override==========================================
    //============================================================================================
    protected override void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("PlayerSO is null", transform.gameObject);
            return;
        }

        // Obj
        this.objName = this.so.ObjName;

        // Character
        this.maxHp = this.so.MaxHp;
        this.moveSpeed = this.so.MoveSpeed;

        // Player
        this.rb.isKinematic = false;
        this.bodyCollider.isTrigger = false;

        this.maxAmor = this.so.MaxAmor;
        this.maxMana = this.so.MaxMana;
        this.dashSpeed = this.so.DashSpeed;

        this.DefaultDash();
        this.DefaultAmorRegen();
    }
}

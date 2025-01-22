using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Bullet : BaseObj, DespawnUser, DespawnByDistanceUser, ChargeByTimeUser, 
    MovementUser, ChaseTargetUser
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Bullet===")]
    [Header("// Stat")]
    [SerializeField] protected Transform shooter;
    [SerializeField] protected BulletAttribute attribute;
    [SerializeField] protected List<FactionType> damgableTypes;
    [SerializeField] protected bool canPierce;

    // Movement
    [Header("// Movement")]
    [SerializeField] protected Movement movement;

    // IdentifyObj
    [Header("IdentifyObj")]
    [SerializeField] protected IdentifyObjByCollide idenObjByCollide;

    // Despawn
    [Header("// Despawn")]
    [SerializeField] protected List<Despawner> despawners;

    // Charge
    [Header("// Charge")]
    [SerializeField] protected ChargeByTime charge;
    [SerializeField] protected List<float> moveSpeeds;
    [SerializeField] protected List<int> damages;
    [SerializeField] protected float sizeMul;

    // Component
    [Header("// Component")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D bodyCollider;

    //==========================================Get Set===========================================
    // Stat
    public BulletAttribute Attribute { get => attribute; set => attribute = value; }
    public List<FactionType> DamagableTypes { get => damgableTypes; set => damgableTypes = value; }
    public bool CanPierce { get => canPierce; set => canPierce = value; }

    // Movement
    public Movement Movement { get => movement; set => movement = value; }

    // IdentifyObjByCollide
    public IdentifyObjByCollide IdenObjByCollide { get => idenObjByCollide; set => idenObjByCollide = value; }

    // Charge
    public ChargeByTime Charge { get => charge; set => charge = value; }
    public List<float> MoveSpeeds { get => moveSpeeds; set => moveSpeeds = value; }
    public List<int> Damages { get => damages; set => damages = value; }
    public float SizeMul { get => sizeMul; set => sizeMul = value; }

    // Component
    public Rigidbody2D Rb { get => rb; }
    public CapsuleCollider2D BodyColldier { get => bodyCollider; }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Load Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.despawners, transform.Find("Despawn"), "LoadDespawners()");
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
        this.LoadComponent(ref this.idenObjByCollide, transform.Find("IdentifyObj"), 
            "LoadIdenObjByCollide()");
        this.LoadComponent(ref this.charge, transform.Find("Charge"), "LoadCharge()");

        // Despawners
        foreach (Despawner despawner in this.despawners)
        {
            despawner.Owner = transform;
            despawner.MyLoadComponent();
        }

        // Movement
        if (this.movement != null)
        {
            this.movement.Owner = transform;
            this.movement.MyLoadComponent();
            this.movement.DefaultStat();
        }

        // IdentifyObjByCollide
        if (this.idenObjByCollide != null)
        {
            this.idenObjByCollide.Owner = transform;
            this.idenObjByCollide.MyLoadComponent();
            this.idenObjByCollide.MyLoadComponent();
        }

        // Charge
        if (this.charge != null)
        {
            this.charge.Owner = transform;
            this.charge.MyLoadComponent();
            this.charge.DefaultStat();
        }

        // Default
        this.DefaultStat();
    }

    protected virtual void OnEnable()
    {
        // Movement
        if (this.movement != null) this.movement.ResetMovement();

        // Despawners
        foreach (Despawner despawner in this.despawners) despawner.ResetDespawn();

        // Charge
        if (this.charge != null) this.charge.ResetCharge();
    }

    protected virtual void Update()
    {
        // Movement
        if (this.movement != null) this.movement.MyUpdate();

        // Despawners
        foreach (Despawner despawner in this.despawners) despawner.MyUpdate();

        // Charge
        if (this.charge != null) this.charge.MyUpdate();
    }

    protected virtual void FixedUpdate()
    {
        // Movement
        if (this.movement != null) this.movement.MyFixedUpdate();

        // Despawner
        foreach (Despawner despawner in this.despawners) despawner.MyFixedUpdate();

        // Charge
        if (this.charge != null) this.charge.MyFixedUpdate();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        DamageReceiver receiver = collision.transform.GetComponent<DamageReceiver>();
        if (receiver != null)
        {
            foreach (FactionType child in this.damgableTypes)
            {
                if (child != receiver.GetFactionType()) continue;
                this.OnColliding(collision.transform);
            }
        }
    }



    //============================================================================================
    //==========================================Movement==========================================
    //============================================================================================

    //=======================================Movement User========================================
    Rigidbody2D MovementUser.GetRb()
    {
        return this.rb;
    }

    //======================================ChaseTarget User======================================
    Transform ChaseTargetUser.GetTarget()
    {
        if (this.idenObjByCollide == null)
        {
            Debug.LogWarning("Require IdentifyObjByCollide for ChaseTargetUser", 
                transform.gameObject);
        }
        
        return this.idenObjByCollide.Target;
    }



    //============================================================================================
    //==========================================Despawn===========================================
    //============================================================================================

    //========================================Despawn User========================================
    Spawner DespawnUser.GetSpawner()
    {
        return BulletSpawner.Instance;
    }

    //==================================Despawn By Distance User==================================
    Vector2 DespawnByDistanceUser.GetTargetPos()
    {
        return this.shooter.position;
    }



    //============================================================================================
    //===========================================Charge===========================================
    //============================================================================================
    void ChargeByTimeUser.OnCharging()
    {
        this.movement.MoveSpeed = this.moveSpeeds[this.charge.GetState(this.moveSpeeds.Count - 1)];
        this.attribute.Damage = this.damages[this.charge.GetState(this.damages.Count)];
        
        transform.localScale *= this.sizeMul;
        this.bodyCollider.size *= this.sizeMul;
    }

    void ChargeByTimeUser.OnFinishCharging()
    {
        this.movement.MoveSpeed = this.moveSpeeds[this.moveSpeeds.Count - 1];
        this.attribute.Damage = this.damages[this.damages.Count - 1];
    }



    //============================================================================================
    //===========================================Other============================================
    //============================================================================================
    // Stat
    protected virtual void DefaultBulletStat(BulletSO bulletSO)
    {
        this.attribute.Damage = bulletSO.Damage;
        this.damgableTypes = bulletSO.DamagableTypes;
    }

    protected virtual void DefaultPoisonEff(BulletSO bulletSO)
    {
        this.attribute.PoisonEffDuration = bulletSO.PoisonEffDuration;
        this.attribute.PoisonDamage = bulletSO.PoisonDamage;
    }

    protected virtual void DefaultFireEff(BulletSO bulletSO)
    {
        this.attribute.FireEffDuration = bulletSO.FireEffDuration;
        this.attribute.FireDamage = bulletSO.FireDamage;
    }

    // Component
    protected virtual void DefaultComponent()
    {
        this.rb.isKinematic = true;
        this.bodyCollider.isTrigger = true;
    }



    //============================================================================================
    //==========================================Override==========================================
    //============================================================================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        BulletSO bulletSO = (BulletSO)this.so;
        if (bulletSO == null)
        {
            Debug.LogError("SO is null", transform.gameObject);
            return;
        }

        this.DefaultBulletStat(bulletSO);
        this.DefaultPoisonEff(bulletSO);
        this.DefaultFireEff(bulletSO);
        this.DefaultComponent();
    }



    //============================================================================================
    //===========================================Method===========================================
    //============================================================================================
    protected virtual void OnColliding(Transform collidedObj)
    {
        HpReceiver hpRecv = collidedObj.GetComponent<HpReceiver>();
        FireEffReceiver fireEffRecv = collidedObj.GetComponent<FireEffReceiver>();
        PoisonEffReceiver poisonEffRecv = collidedObj.GetComponent<PoisonEffReceiver>();

        if (hpRecv != null) 
            hpRecv.ReceiveHp(-this.attribute.Damage);
        if (fireEffRecv != null) 
            fireEffRecv.ReceiveFireEff(this.attribute.FireEffDuration, this.attribute.FireDamage);
        if (poisonEffRecv != null) 
            poisonEffRecv.ReceivePoisonEff(this.attribute.PoisonEffDuration, this.attribute.PoisonDamage);

        if (hpRecv == null && fireEffRecv == null && poisonEffRecv == null) return;
        if (this.canPierce) return;
        DespawnUtil.Instance.Despawn(transform, BulletSpawner.Instance);
    }

    public virtual void SetShooter(Transform obj)
    {
        this.shooter = obj;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Bullet : BaseObj, DespawnUser, DespawnByDistanceUser, ChargeByTimeUser
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Bullet===")]
    [Header("// Stat")]
    [SerializeField] protected List<FactionType> damgableTypes;
    [SerializeField] protected bool canPierce;

    // Movement
    [Header("// Movement")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool canMove;

    // Effect
    [Header("// Instant Damage")]
    [SerializeField] protected int damage;

    [Header("// Fire Effect")]
    [SerializeField] protected float fireEffDuration;
    [SerializeField] protected int fireDamage;

    [Header("// Poison Effect")]
    [SerializeField] protected float poisonEffDuration;
    [SerializeField] protected int poisonDamage;

    // Despawn
    [Header("// Despawn")]
    [SerializeField] protected List<Despawner> despawners;
    [Header("// Despawn By Time")]
    [SerializeField] protected Cooldown despawnByTimeCD;
    [SerializeField] protected bool canDespawnByTime;

    [Header("// Despawn By Distance")]
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float despawnDistance;
    [SerializeField] protected float currDistance;
    [SerializeField] protected bool canDespawnByDistance;

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
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    
    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public List<FactionType> DamagableTypes
    {
        get => damgableTypes;
        set => damgableTypes = value;
    }

    public bool CanPierce
    {
        get => canPierce;
        set => canPierce = value;
    }

    // Movement
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    // Poison Effect
    public float PoisonEffDuration
    {
        get => poisonEffDuration;
        set => poisonEffDuration = value;
    }

    public int PoisonDamage
    {
        get => poisonDamage;
        set => poisonDamage = value;
    }

    // Fire Effect
    public float FireEffDuration
    {
        get => fireEffDuration;
        set => fireEffDuration = value;
    }

    public int FireDamage
    {
        get => fireDamage;
        set => fireDamage = value;
    }

    // Despawn By Time
    public bool CanDespawnByTime
    {
        get => canDespawnByTime;
        set => canDespawnByTime = value;
    }

    // Component
    public Rigidbody2D Rb
    {
        get => rb;
    }

    public CapsuleCollider2D BodyColldier
    {
        get => bodyCollider;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Load Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.despawners, transform.Find("Despawn"), "LoadDespawners()");

        // Despawners
        foreach (Despawner despawner in this.despawners)
        {
            despawner.Owner = transform;
            despawner.MyLoadComponent();
        }

        // Default
        this.DefaultStat();
    }

    protected virtual void OnEnable()
    {
        this.Respawn();
    }

    protected virtual void Update()
    {
        foreach (Despawner despawner in this.despawners) despawner.MyUpdate();
    }

    protected virtual void FixedUpdate()
    {
        this.DespawnByTime();
        this.DespawnByDistance();

        // Despawner
        foreach (Despawner despawner in this.despawners) despawner.MyFixedUpdate();
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

    //==========================================Movement==========================================
    protected virtual void DefaultMovement()
    {
        this.canMove = false;
    }

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

    //======================================Despawn By Time=======================================
    protected virtual void DespawnByTime()
    {
        if (!this.canDespawnByTime) return;
        DespawnUtil.Instance.DespawnByTime(this.despawnByTimeCD, transform, BulletSpawner.Instance);
    }

    //======================================Despawn By Distance====================================
    protected virtual void DespawnByDistance()
    {
        if (!this.canDespawnByDistance || this.shooter == null) return;
        this.currDistance = DespawnUtil.Instance.DespawnByDistance
            (transform.position, this.shooter.position, this.despawnDistance, transform, BulletSpawner.Instance);
    }

    public virtual void SetShooter(Transform obj) 
    {
        this.shooter = obj;
    }

    //===========================================Charge===========================================
    void ChargeByTimeUser.OnCharging()
    {
        transform.localScale *= this.sizeMul;
        this.bodyCollider.size *= this.sizeMul;
    }

    void ChargeByTimeUser.OnFinishCharging()
    {
        
    }

    //===========================================Other============================================
    // Respawn
    protected virtual void Respawn()
    {
        this.canMove = true;
        this.despawnByTimeCD.ResetStatus();
    }
    
    // Default
    protected virtual void DefaultBulletStat(BulletSO bulletSO)
    {
        this.moveSpeed = bulletSO.MoveSpeed;
        this.damage = bulletSO.Damage;
        this.damgableTypes = bulletSO.DamagableTypes;
    }

    protected virtual void DefaultPoisonEff(BulletSO bulletSO)
    {
        this.poisonEffDuration = bulletSO.PoisonEffDuration;
        this.poisonDamage = bulletSO.PoisonDamage;
    }

    protected virtual void DefaultFireEff(BulletSO bulletSO)
    {
        this.fireEffDuration = bulletSO.FireEffDuration;
        this.fireDamage = bulletSO.FireDamage;
    }

    // Despawn
    protected virtual void DefaultDespawnByTime(BulletSO bulletSO)
    {
        this.canDespawnByTime = true;
        this.despawnByTimeCD = new Cooldown(bulletSO.DespawnTime, Time.fixedDeltaTime);
    }

    protected virtual void DefaultDespawnByDistance(BulletSO bulletSO)
    {
        this.canDespawnByDistance = true;
        this.despawnDistance = bulletSO.DespawnDistance;
    }


    // Movement
    protected virtual void DefaultMovement(BulletSO bulletSO)
    {
        this.canMove = false;
    }

    // Component
    protected virtual void DefaultComponent()
    {
        this.rb.isKinematic = true;
        this.bodyCollider.isTrigger = true;
    }

    //==========================================Override==========================================
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
        this.DefaultDespawnByTime(bulletSO);
        this.DefaultDespawnByDistance(bulletSO);
        this.DefaultMovement(bulletSO);
        this.DefaultComponent();
    }

    //==========================================Abstract==========================================
    protected virtual void OnColliding(Transform collidedObj)
    {
        HpReceiver hpRecv = collidedObj.GetComponent<HpReceiver>();
        FireEffReceiver fireEffRecv = collidedObj.GetComponent<FireEffReceiver>();
        PoisonEffReceiver poisonEffRecv = collidedObj.GetComponent<PoisonEffReceiver>();

        if (hpRecv != null) hpRecv.ReceiveHp(-this.damage);
        if (fireEffRecv != null) fireEffRecv.ReceiveFireEff(this.fireEffDuration, this.fireDamage);
        if (poisonEffRecv != null) poisonEffRecv.ReceivePoisonEff(this.poisonEffDuration, this.poisonDamage);

        if (hpRecv == null && fireEffRecv == null && poisonEffRecv == null) return;
        if (this.canPierce) return;
        DespawnUtil.Instance.Despawn(transform, BulletSpawner.Instance);
    }
}

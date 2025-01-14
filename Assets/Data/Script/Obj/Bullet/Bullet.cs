using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Bullet : BaseObj, HpSender
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Bullet===")]
    // Stat
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected List<DamagableType> damgableTypes;

    // Movement
    [SerializeField] protected bool canMove;

    // Poison Effect
    [SerializeField] protected float fireEffDuration;
    [SerializeField] protected int fireDamage;

    // Fire Effect
    [SerializeField] protected float poisonEffDuration;
    [SerializeField] protected int poisonDamage;


    // Despawn By Time
    [SerializeField] protected Cooldown despawnByTimeCD;
    [SerializeField] protected bool canDespawnByTime;

    // Despawn By Distance
    [SerializeField] protected Transform gunObj;
    [SerializeField] protected float despawnDistance;
    [SerializeField] protected bool canDespawnByDistance;

    // Component
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

    public List<DamagableType> DamagableTypes
    {
        get => damgableTypes;
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
        // Default
        this.DefaultStat();

        // Load Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
    }

    protected virtual void OnEnable()
    {
        this.Respawn();
    }

    protected virtual void FixedUpdate()
    {
        this.DespawnByTime();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        DamageReceiver receiver = collision.transform.GetComponent<DamageReceiver>();
        if (receiver != null)
        {
            foreach (DamagableType child in this.damgableTypes)
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

    //==========================================HpSender==========================================
    void HpSender.Send(HpReceiver receiver) 
    {
        receiver.ReceiveHp(this.damage);
        DespawnUtil.Instance.Despawn(transform, BulletSpawner.Instance);
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
        if (!this.canDespawnByDistance) return;
        DespawnUtil.Instance.DespawnByDistance(transform.position, this.gunObj.position, transform, BulletSpawner.Instance);
    }

    public virtual void SetGunObj(Transform obj) 
    {
        this.gunObj = obj;
    }

    //===========================================Other============================================
    // Respawn
    protected virtual void Respawn()
    {
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

    protected virtual void DefaultMovement(BulletSO bulletSO)
    {
        this.canMove = false;
    }

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
    protected abstract void OnColliding(Transform collidedObj);
}

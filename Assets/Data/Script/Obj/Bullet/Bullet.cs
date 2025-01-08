using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Bullet : BaseObj, HpSender
{
    //==========================================Variable==========================================
    [Header("Bullet")]
    // Stat
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected float despawnTime;
    [SerializeField] protected float despawnDistance;
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

    // Component
    [SerializeField] protected BulletSO so;
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

    public float DespawnTime
    { 
        get => despawnTime;
        set => despawnTime = value;
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
        receiver.Receive(this.damage);
        DespawnUtil.Instance.Despawn(transform, BulletSpawner.Instance);
    }

    //======================================Despawn By Time=======================================
    protected virtual void DespawnByTime()
    {
        if (!this.canDespawnByTime) return;
        DespawnUtil.Instance.DespawnByTime(this.despawnByTimeCD, transform, BulletSpawner.Instance);
    }

    protected virtual void DefaultDespawnByTime()
    {
        this.canDespawnByTime = false;
        this.despawnByTimeCD = new Cooldown(this.despawnTime, Time.fixedDeltaTime);
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("SO is null", transform.gameObject);
            return;
        }

        // Obj
        this.objName = this.so.ObjName;
        
        // Stat
        this.moveSpeed = this.so.MoveSpeed;
        this.damage = this.so.Damage;
        this.despawnTime = this.so.DespawnTime;
        this.despawnDistance = this.so.DespawnDistance;

        this.DefaultMovement();
        this.DefaultDespawnByTime();
    }

    //==========================================Abstract==========================================
    protected abstract void OnColliding(Transform collidedObj);
}

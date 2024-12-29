using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bullet : BaseProjectile
{
    //==========================================Variable==========================================
    [Header("Bullet Obj")]
    // Stat
    [SerializeField] private float moveSpeed;
    [SerializeField] private float despawnTime;
    [SerializeField] private Vector2 despawnRange;
    [SerializeField] private bool canMove;

    // Unity Component
    [SerializeField] private Rigidbody2D rb;

    // Child Component
    [SerializeField] private BulletSO so;
    [SerializeField] private BulletMovement movement;
    [SerializeField] private BulletDespawn despawn;
    [SerializeField] private BulletStat stat;

    //==========================================Get Set===========================================
    // Stat
    public float MoveSpeed => moveSpeed;
    public float DespawnTime => despawnTime;
    public Vector2 DespawnRange => despawnRange;
    public bool CanMove => canMove;
    
    // Unity Component
    public Rigidbody2D Rb => rb;

    // Child Component
    public BulletMovement Movement => movement;
    public BulletDespawn Despawn => despawn;
    public BulletStat Stat => stat;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()");
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");

        // Child Component
        this.LoadSO(ref this.so, "SO/NonLiving/Projectile/Bullet/" + transform.name);
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
        this.LoadComponent(ref this.despawn, transform.Find("Despawn"), "LoadDespawn()");
        this.LoadComponent(ref this.stat, transform.Find("Stat"), "LoadStat()");

        // Stat
        this.DefaultStat();
    }

    private void OnEnable()
    {
        this.DefaultStat();
        this.despawn.Handle();
    }

    //==========================================Movement==========================================
    public void PerformMove()
    {
        this.canMove = true;
    }

    //===========================================Other============================================
    private void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("SO is null", transform.gameObject);
            return;
        }

        // Stat
        this.objName = this.so.ObjName;
        this.canGoThrough = this.so.CanGoThrough;
        this.despawnRange = this.so.DespawnRange;
        this.despawnTime = this.so.DespawnTime;
        this.moveSpeed = this.so.MoveSpeed;
        this.canMove = false;

        // Unity Component
        this.rb.isKinematic = true;
        this.bodyCollider.isTrigger = false;

        // Child Component
        this.movement.DefaultStat();
        this.despawn.DefaultStat();
        this.stat.DefaultStat();
    }
}

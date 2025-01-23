using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableBullet : Bullet, ChargeOnStart, ChargeOnMoving
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Chargable Bullet===")]
    [Header("// Stat")]
    [SerializeField] protected Cooldown chargeCD;
    [SerializeField] protected List<float> moveSpeeds;
    [SerializeField] protected List<int> damages;
    [SerializeField] protected float minSize;
    [SerializeField] protected float maxSize;
    [SerializeField] protected float sizeDiff; // bodyCollider.size / transform.size
    [SerializeField] protected bool canCharge;

    //==========================================Get Set===========================================
    // Charge
    public Cooldown ChargeCD { get => chargeCD; set => chargeCD = value; }
    public List<float> MoveSpeeds { get => moveSpeeds; set => moveSpeeds = value; }
    public List<int> Damages { get => damages; set => damages = value; }
    public float MinSize { get => minSize; set => minSize = value; }
    public float MaxSize { get => maxSize; set => maxSize = value; }
    public float SizeDiff { get => sizeDiff; set => sizeDiff = value; }
    public bool CanCharge { get => canCharge; set => canCharge = value; }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSO(ref this.so, "SO/Obj/Projectile/Bullet/Chargable/" + transform.name);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (this.canCharge) this.chargeCD.CoolingDown();
    }

    //===========================================Method===========================================
    // Charge
    protected virtual void DefaultCharge(ChargableBulletSO chargableBulletSO)
    {
        this.chargeCD = new Cooldown(chargableBulletSO.ChargeTime, Time.fixedDeltaTime);
        this.moveSpeeds = chargableBulletSO.MoveSpeeds;
        this.damages = chargableBulletSO.Damages;
        this.minSize = chargableBulletSO.MinSize;
        this.maxSize = chargableBulletSO.MaxSize;
        this.sizeDiff = chargableBulletSO.SizeDiff;
    }



    //============================================================================================
    //=========================================Interface==========================================
    //============================================================================================

    //======================================Charge On Start=======================================
    void ChargeOnStart.ActivateCharge()
    {
        this.movement.CanMove = false;
    }

    void ChargeOnStart.OnCharging(float totalTime, float currTime)
    {
        int moveSpeedState = (int)(currTime / totalTime * (this.moveSpeeds.Count - 1));
        int damageState = (int)(currTime / totalTime * (this.damages.Count - 1));

        this.movement.MoveSpeed = this.moveSpeeds[moveSpeedState];
        this.attribute.Damage = this.damages[damageState];
        transform.localScale = Vector3.one * (this.minSize + currTime / totalTime 
            * (this.maxSize - this.minSize));
        this.bodyCollider.size = transform.localScale * this.sizeDiff;
    }

    void ChargeOnStart.FinishCharge()
    {
        this.movement.CanMove = true;
    }

    //======================================Charge On Moving======================================
    void ChargeOnMoving.ActivateCharge()
    {
        this.movement.CanMove = true;
    }

    void ChargeOnMoving.OnCharging(float totalTime, float currTime)
    {
        int moveSpeedState = (int)(currTime / totalTime * (this.moveSpeeds.Count - 1));
        int damageState = (int)(currTime / totalTime * (this.damages.Count - 1));

        this.movement.MoveSpeed = this.moveSpeeds[moveSpeedState];
        this.attribute.Damage = this.damages[damageState];
        transform.localScale = Vector3.one * (this.minSize + currTime / totalTime
            * (this.maxSize - this.minSize));
        this.bodyCollider.size = transform.localScale * this.sizeDiff;
    }

    void ChargeOnMoving.FinishCharge()
    {
        
    }



    //============================================================================================
    //==========================================Override==========================================
    //============================================================================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        ChargableBulletSO bulletSO = (ChargableBulletSO)this.so;
        if (bulletSO == null)
        {
            Debug.LogError("ChargableBulletSO is null", transform.gameObject);
            return;
        }

        this.DefaultCharge(bulletSO);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "SO/Obj/Projectile/Bullet/Chargable")]
public class ChargableBulletSO : BulletSO
{
    //==========================================Variable==========================================
    [Header("Chargable")]
    // Charge
    [SerializeField] protected float chargeTime;
    [SerializeField] protected List<float> moveSpeeds;
    [SerializeField] protected List<int> damages;
    [SerializeField] protected float minSize;
    [SerializeField] protected float maxSize;
    [SerializeField] protected float sizeDiff; // bodyCollider.size / transform.size

    //============================================Get=============================================
    //Charge
    public float ChargeTime => chargeTime;
    public List<float> MoveSpeeds => moveSpeeds;
    public List<int> Damages => damages;
    public float MinSize => minSize;
    public float MaxSize => maxSize;
    public float SizeDiff => sizeDiff;
}

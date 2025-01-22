using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeByTime", menuName = "SO/Component/Charge")]
public class ChargeByTimeSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("Charge By Time")]
    [SerializeField] private float chargeTime;

    //============================================Get=============================================
    public float ChargeTime => chargeTime;
}

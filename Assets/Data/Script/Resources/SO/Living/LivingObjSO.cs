using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObjSO : ObjSO
{
    [Header("Living Obj SO")]
    [SerializeField] private float health;

    public float Health => health;
}

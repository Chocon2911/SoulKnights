using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetSO : MovementSO
{
    //==========================================Variable==========================================
    [Header("Chase Target")]
    [SerializeField] private float rotateSpeed;

    //============================================Get=============================================
    public float RotateSpeed => rotateSpeed;
}

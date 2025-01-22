using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetByCollideSO : ChaseTargetSO
{
    //==========================================Variable==========================================
    [Header("Chase Target By Collide")]
    [SerializeField] private IdentifyObjByCollideSO idenObjByCollideSO;

    //============================================Get=============================================
    public IdentifyObjByCollideSO IdenObjByCollideSO => idenObjByCollideSO;
}

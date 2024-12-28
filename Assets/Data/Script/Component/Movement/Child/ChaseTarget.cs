using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : BaseMovement
{
    //==========================================Variable==========================================
    [SerializeField] private Transform target;

    //==========================================Override==========================================
    protected override Vector2 GetDir()
    {
        Vector2 dir = target.position - this.transform.position;
        dir.Normalize();

        return dir;
    }
}

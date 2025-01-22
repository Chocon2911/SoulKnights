using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    //============================================Get=============================================
    public float MoveSpeed => moveSpeed;
}

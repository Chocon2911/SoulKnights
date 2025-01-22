using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "SO/Component/Movement/Base")]
public class MovementSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    //============================================Get=============================================
    public float MoveSpeed => moveSpeed;
}

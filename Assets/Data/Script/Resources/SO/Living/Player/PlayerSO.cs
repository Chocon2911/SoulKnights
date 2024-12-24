using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "SO/Living/Player")]
public class PlayerSO : ObjSO
{
    [Header("Player SO")]
    [SerializeField] private float moveSpeed;

    public float MoveSpeed => moveSpeed;
}

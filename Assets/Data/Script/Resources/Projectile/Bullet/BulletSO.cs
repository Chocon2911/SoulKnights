using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "SO/Bullet")]
public class BulletSO : ObjSO
{
    //==========================================Variable==========================================
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float despawnTime;
    [SerializeField] private float despawnDistance;

    //============================================Get=============================================
    public float MoveSpeed => moveSpeed;
    public int Damage => damage;
    public float DespawnTime => despawnTime;
    public float DespawnDistance => despawnDistance;
}

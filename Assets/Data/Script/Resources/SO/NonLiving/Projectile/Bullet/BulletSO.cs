using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "SO/NonLiving/Projectile/Bullet")]
public class BulletSO : ProjectileSO
{
    [Header("Bullet")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 despawnRange;
    [SerializeField] private float despawnTime;

    public float MoveSpeed => moveSpeed;
    public Vector2 DespawnRange => despawnRange;
    public float DespawnTime => despawnTime;
}

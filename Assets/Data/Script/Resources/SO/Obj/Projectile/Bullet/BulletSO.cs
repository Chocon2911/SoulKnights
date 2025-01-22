using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "SO/Bullet")]
public class BulletSO : ObjSO
{
    //==========================================Variable==========================================
    [Header("Bullet")]
    // Stat
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float despawnTime;
    [SerializeField] private float despawnDistance;
    [SerializeField] private List<FactionType> damgableTypes;

    // Poison Effect
    [SerializeField] protected float fireEffDuration;
    [SerializeField] protected int fireDamage;

    // Fire Effect
    [SerializeField] protected float poisonEffDuration;
    [SerializeField] protected int poisonDamage;

    //============================================Get=============================================
    // Stat
    public float MoveSpeed => moveSpeed;
    public int Damage => damage;
    public float DespawnTime => despawnTime;
    public float DespawnDistance => despawnDistance;
    public List<FactionType> DamagableTypes => damgableTypes;

    // Poison Effect
    public float PoisonEffDuration => poisonEffDuration;
    public int PoisonDamage => poisonDamage;

    // Fire Effect
    public float FireEffDuration => fireEffDuration;
    public int FireDamage => fireDamage;
}

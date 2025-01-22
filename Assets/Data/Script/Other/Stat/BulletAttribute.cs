using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttribute
{
    //==========================================Variable==========================================
    [Header("// Instant Damage")]
    [SerializeField] protected int damage;

    [Header("// Fire Effect")]
    [SerializeField] protected float fireEffDuration;
    [SerializeField] protected int fireDamage;

    [Header("// Poison Effect")]
    [SerializeField] protected float poisonEffDuration;
    [SerializeField] protected int poisonDamage;

    //==========================================Get Set===========================================
    // Instant Damage
    public int Damage { get => damage; set => damage = value; }

    // Poison Effect
    public float PoisonEffDuration { get => poisonEffDuration; set => poisonEffDuration = value; }
    public int PoisonDamage { get => poisonDamage; set => poisonDamage = value; }

    // Fire Effect
    public float FireEffDuration { get => fireEffDuration; set => fireEffDuration = value; }
    public int FireDamage { get => fireDamage; set => fireDamage = value; }

    //========================================Constructor=========================================
    public BulletAttribute(int damage, float fireEffDuration, int fireDamage, 
        float poisonEffDuration, int poisonDamage)
    {
        this.damage = damage;
        this.fireEffDuration = fireEffDuration;
        this.fireDamage = fireDamage;
        this.poisonEffDuration = poisonEffDuration;
        this.poisonDamage = poisonDamage;
    }
}

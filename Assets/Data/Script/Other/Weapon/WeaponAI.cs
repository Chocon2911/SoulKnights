using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAI
{
    [SerializeField] private Cooldown cd;
    [SerializeField] private bool canAttack;

    public Cooldown Cd { get => cd; set => cd = value; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }

    public WeaponAI(Cooldown cd, bool canAttack)
    {
        this.cd = cd;
        this.canAttack = canAttack;
    }

    public void WeaponHolding()
    {

    }

    public void WeaponHandling()
    {

    }
}

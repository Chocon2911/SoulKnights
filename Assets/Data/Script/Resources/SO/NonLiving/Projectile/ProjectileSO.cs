using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSO : NonLivingSO
{
    [Header("Projectile")]
    [SerializeField] private bool canGoThrough;

    public bool CanGoThrough => canGoThrough;
}

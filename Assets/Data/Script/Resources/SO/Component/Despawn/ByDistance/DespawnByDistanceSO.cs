using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DespawnByDistance", menuName = "SO/Component/Despawn/ByDistance")]
public class DespawnByDistanceSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("By Distance")]
    [SerializeField] private float despawnDistance;

    //============================================Get=============================================
    public float DespawnDistance => despawnDistance;
}

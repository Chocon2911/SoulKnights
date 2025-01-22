using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DespawnByTime", menuName = "SO/Component/Despawn/ByTime")]
public class DespawnByTimeSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("By Time")]
    [SerializeField] private float despawnTime;

    //============================================Get=============================================
    public float DespawnTime => despawnTime;
}

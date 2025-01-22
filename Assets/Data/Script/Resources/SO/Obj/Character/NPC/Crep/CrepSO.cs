using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crep", menuName = "SO/Character/NPC/Crep")]
public class CrepSO : NPCSO
{
    //==========================================Variable==========================================
    [Header("Crep")]
    [SerializeField] private float minMoveRandomCD;
    [SerializeField] private float maxMoveRandomCD;
    [SerializeField] private float randomRange;

    //============================================Get=============================================
    public float MinMoveRandomCD => minMoveRandomCD;
    public float MaxMoveRandomCD => maxMoveRandomCD;
    public float RandomRange => randomRange;
}

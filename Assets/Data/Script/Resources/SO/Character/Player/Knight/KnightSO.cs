using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "SO/Character/Player/Knight")]
public class KnightSO : PlayerSO
{
    [Header("Knight")]
    [SerializeField] private DualWieldSkillSO dualWield;
    public DualWieldSkillSO DualWield => dualWield;
}

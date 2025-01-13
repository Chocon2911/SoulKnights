using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : BaseCharacter
{
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===NPC===")]
    [Header("Identify Target")]
    [SerializeField] protected IdentifyObjByCollide itentifyTarget;

    [Header("Move Randomly")]


    [Header("Weapon")]
    [SerializeField] protected Weapon weapon;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSO : CharacterSO
{
    //==========================================Variable==========================================
    [Header("NPC")]
    [SerializeField] private FactionType faction;
    [SerializeField] private IdentifyObjByCollideSO identifyObjByCollideSO;

    //============================================Get=============================================
    public FactionType Faction => faction;

    public IdentifyObjByCollideSO IdentifyObjByCollideSO => identifyObjByCollideSO;
}

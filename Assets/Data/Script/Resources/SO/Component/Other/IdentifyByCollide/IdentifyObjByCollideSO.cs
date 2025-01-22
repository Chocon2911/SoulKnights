using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "SO/Component/Other/IdentifyObjByCollide")]
public class IdentifyObjByCollideSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("Identify Obj By Collide")]
    [SerializeField] private float detectRange;
    [SerializeField] private List<string> tags;

    //============================================Get=============================================
    public float DetectRange => detectRange;
    public List<string> Tags => tags;
}

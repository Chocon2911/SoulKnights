using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSO : ScriptableObject
{
    [Header("Obj SO")]
    [SerializeField] private string objName;
    [SerializeField] private SpriteRenderer model;

    public string ObjName => objName;
    public SpriteRenderer Model => model;
}

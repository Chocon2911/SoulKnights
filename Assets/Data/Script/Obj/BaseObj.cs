using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObj : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("===Obj===")]
    // Stat
    [SerializeField] protected string id;
    [SerializeField] protected string objName;

    // Component
    [SerializeField] protected ObjSO so;
    [SerializeField] protected SpriteRenderer image;
    [SerializeField] protected Animator myAnimator;

    //==========================================Get Set===========================================
    public string Id
    {
        get => id;
        set => id = value;
    }

    public string ObjName
    {
        get => objName;
        set => ObjName = value;
    }

    public SpriteRenderer Image
    {
        get => image;
    }
    public Animator MyAnimator
    {
        get => myAnimator;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.image, transform.Find("Model"), "LoadImage()");
        this.LoadComponent(ref this.myAnimator, transform.Find("Model"), "LoadAnimator()");
    }

    //==========================================Abstract==========================================
    protected virtual void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("ObjSO is null", transform.gameObject);
            return;
        }

        this.id = this.so.Id;
        this.objName = this.so.ObjName;
    }
}

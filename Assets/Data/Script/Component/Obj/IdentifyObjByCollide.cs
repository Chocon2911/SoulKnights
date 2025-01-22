using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class IdentifyObjByCollide : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] private CircleCollider2D identifyZone;
    [SerializeField] private Transform owner;
    [SerializeField] private List<string> tags;
    [SerializeField] private Transform target;

    //==========================================Get Set===========================================
    public CircleCollider2D IdentifyZone { get => identifyZone; set => identifyZone = value; }
    public Transform Owner { get => owner; set => owner = value; }
    public List<string> Tags { get => tags; set => tags = value; }
    public Transform Target { get => target; set => target = value; }

    //===========================================Unity============================================
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform == this.owner 
            || collision.transform.IsChildOf(this.owner)
            || target != null) return;

        foreach (string tag in this.tags)
        {
            if (tag != collision.tag) continue;
            this.target = collision.transform;
            break;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.identifyZone, transform, "LoadIdentifyZone()");
    }

    //===========================================Method===========================================
    public void MyLoadComponent()
    {
        this.LoadComponent(ref this.identifyZone, transform, "LoadIdentifyZone()");
    }

    public void DefaultStat(IdentifyObjByCollideSO so)
    {
        this.identifyZone.radius = so.DetectRange;
        this.tags = so.Tags;
        this.identifyZone.isTrigger = true;
    }
}

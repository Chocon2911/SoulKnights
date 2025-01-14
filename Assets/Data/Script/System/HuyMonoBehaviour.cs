using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuyMonoBehaviour : MonoBehaviour
{
    //===========================================Method===========================================
    protected virtual void ResetValue()
    {
        //For override
    }

    protected virtual void LoadComponents()
    {
        //For override
    }

    protected void LoadComponent<T>(ref T component, Transform obj, string message)
    {
        if (component == null || component.Equals(null))
        {
            component = obj.GetComponent<T>();
            Debug.LogWarning(transform.name + ": " + message, transform.gameObject);
        }
    }

    protected virtual void LoadComponent<T>(ref List<T> components, Transform obj, string message)
    {
        if (components.Count > 0) return;
        foreach (Transform child in obj) components.Add(child.GetComponent<T>());
        Debug.LogWarning(transform.name + ": " + message, transform.gameObject);
    }

    protected void LoadChildComponent<T>(ref T component, Transform obj, string message)
    {
        if (component == null || component.Equals(null))
        {
            component = obj.GetComponentInChildren<T>();
            Debug.LogWarning(transform.name + ": " + message, transform.gameObject);
        }
    }

    protected virtual void LoadSO<T>(ref T so, string filePath) where T : ScriptableObject
    {
        if (so == null || so.Equals(null))
        {
            so = Resources.Load<T>(filePath);
            Debug.LogWarning(transform.name + ": LoadSO()", transform.gameObject);
        }
    }

    //===========================================Unity============================================

    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuyMonoBehaviour : MonoBehaviour
{
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

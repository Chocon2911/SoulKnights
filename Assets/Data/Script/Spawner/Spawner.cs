using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class Spawner : HuyMonoBehaviour
{
    [Header("Spawner")]
    [Header("Other")]
    [SerializeField] protected Transform prefabObj;
    public Transform PrefabObj => prefabObj;

    [SerializeField] protected Transform holderObj;
    public Transform HolderObj => holderObj;

    [SerializeField] protected List<Transform> prefabs;
    public List<Transform> Prefabs => prefabs;

    [SerializeField] protected List<Transform> holders;
    public List<Transform> Holders => holders;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.LoadHolders();
    }

    //=======================================Load Component=======================================
    protected virtual void LoadPrefabObj()
    {
        if (this.prefabObj != null) return;
        this.prefabObj = transform.Find("Prefab");
        Debug.LogWarning(transform.name + ": Load PrefabObj", transform.gameObject);
    }

    protected virtual void LoadHolderObj()
    {
        if (this.holderObj != null) return;
        this.holderObj = transform.Find("Holder");
        Debug.LogWarning(transform.name + ": Load HolderObj", transform.gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        this.LoadPrefabObj();

        foreach (Transform obj in this.prefabObj)
        {
            this.prefabs.Add(obj);
            obj.gameObject.SetActive(false);
        }
        Debug.LogWarning(transform.name + ": Load Prefabs", transform.gameObject);
    }

    protected virtual void LoadHolders()
    {
        if (this.holders.Count > 0) return;
        this.LoadHolderObj();

        foreach (Transform obj in this.holderObj)
        {
            this.holders.Add(obj);
            obj.gameObject.SetActive(false);
        }
        //Debug.LogWarning(transform.name + ": Load Holders", transform.gameObject);
    }

    //===========================================Spawn============================================
    public virtual Transform SpawnByName(string objName, Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform spawnObj = this.GetObjByName(objName);
        Transform newObj = this.SpawnByObj(spawnObj, spawnPos, spawnRot);
        return newObj;
    }

    public virtual Transform SpawnByObj(Transform spawnObj, Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform newObj = this.GetObjFromHolder(spawnObj);

        newObj.SetParent(this.holderObj);
        newObj.SetPositionAndRotation(spawnPos, spawnRot);
        return newObj;
    }

    //==========================================Despawn===========================================
    public virtual void Despawn(Transform obj)
    {
        this.holders.Add(obj);
        obj.SetParent(this.holderObj);
        obj.gameObject.SetActive(false);
    }

    //============================================Get=============================================
    protected virtual Transform GetObjByName(string objName)
    {
        foreach (Transform obj in this.prefabs)
        {
            if (obj.name == objName) return obj;
        }

        Debug.LogError(transform.name + ": Can't find any obj with name = " + objName, transform.gameObject);
        return null;
    }

    protected virtual Transform GetObjFromHolder(Transform spawnObj)
    {
        foreach (Transform obj in this.holders)
        {
            if (obj.name == spawnObj.name)
            {
                //Debug.Log(transform.name + ": From Holder", transform.gameObject);
                this.holders.Remove(obj);
                return obj;
            }
        }

        //Debug.Log(transform.name + ": Create new", transform.gameObject);
        Transform newObj = Instantiate(spawnObj);
        newObj.name = spawnObj.name;
        return newObj;
    }
}
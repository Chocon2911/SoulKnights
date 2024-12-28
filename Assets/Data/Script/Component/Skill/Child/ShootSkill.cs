using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootSkill : BaseSkill
{
    //==========================================Variable==========================================
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private Transform bulletObj;


    //========================================Constructor=========================================
    public ShootSkill(float cooldownTime, Transform bulletObj)
    {
        this.cooldown = new Cooldown(cooldownTime);
        this.bulletObj = bulletObj;
    }

    //==========================================Override==========================================
    public void PerformSkill(Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform newBulletObj =  BulletSpawner.Instance.SpawnByObj(this.bulletObj, spawnPos, spawnRot);
        if (newBulletObj == null)
        {
            Debug.LogError("new BulletObj is null");
            return;
        }

        BulletObj bullet = newBulletObj.GetComponent<BulletObj>();
        newBulletObj.gameObject.SetActive(true);
        bullet.PerformMove();
    }
}

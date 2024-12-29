using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotSkill : ShootSkill
{
    //==========================================Variable==========================================
    [SerializeField] private Transform bulletObj;

    //==========================================Override==========================================
    protected override void ActivateSkill(Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform newBulletObj = BulletSpawner.Instance.SpawnByObj(this.bulletObj, spawnPos, spawnRot);
        if (newBulletObj == null)
        {
            Debug.LogError("new BulletObj is null");
            return;
        }

        Bullet bullet = newBulletObj.GetComponent<Bullet>();
        newBulletObj.gameObject.SetActive(true);
        bullet.PerformMove();
    }
}

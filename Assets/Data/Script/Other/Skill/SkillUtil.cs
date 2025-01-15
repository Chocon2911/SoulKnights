using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillUtil
{
    //==========================================Variable==========================================
    private static SkillUtil instance;

    //==========================================Get Set===========================================
    public static SkillUtil Instance
    {
        get
        {
            if (instance == null) instance = new SkillUtil();
            return instance;
        }
    }

    //========================================Constructor=========================================
    public SkillUtil()
    {
        if (instance != null)
        {
            Debug.LogError("One SkillUtil Only");
            return;
        }

        instance = this;
    }

    //========================================Shoot Skill=========================================
    public Transform Shoot(Transform bullet, Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform newBullet = BulletSpawner.Instance.SpawnByObj(bullet, spawnPos, spawnRot);
        if (newBullet == null)
        {
            Debug.LogError("Shoot(): newBullet is null", bullet.gameObject);
        }

        return newBullet;
    }

    public List<Transform> Shotgun(Transform bullet, int bulletCount, Vector3 spawnPos, float shootAngle)
    {
        float halfAngle = shootAngle / 2;
        List<Transform> bullets = new List<Transform>();
        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion spawnRot = Quaternion.Euler(0, 0, spawnPos.z + (i * shootAngle) - halfAngle);
            Transform newBullet = BulletSpawner.Instance.SpawnByObj(bullet, spawnPos, spawnRot);
            if (newBullet == null) return null;

            bullets.Add(newBullet);
        }

        return bullets;
    }

    //===========================================Other============================================
    public void ConsumeHp(HpReceiver receiver, Skill skill)
    {
        if (receiver == null) return;
        if (receiver.GetCurrHp() <= skill.HpCost) return;
        receiver.ReceiveHp(-skill.HpCost);
    }

    public void ConsumeMana(ManaReceiver receiver, Skill skill) 
    {
        if (receiver == null) return;
        receiver.ReceiveMana(-skill.ManaCost);
    }
}

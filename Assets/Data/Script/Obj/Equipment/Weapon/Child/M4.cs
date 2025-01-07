using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : Weapon, IAttackable
{
    //==========================================Variable==========================================
    [SerializeField] private float fireRate;
    [SerializeField] private Transform bullet;
    [SerializeField] private Skill skill;

    //==========================================Get Set===========================================
    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    //========================================IAttackable=========================================
    public void Attack(Player player, int state)
    {
        if (state <= 0 || !this.skill.CanUseSkill(player)) return;
        
        Transform newBullet = SkillUtil.Instance.Shoot(bullet, transform.position, transform.rotation);
        if (newBullet == null) return;
        
        newBullet.gameObject.SetActive(true);
        SkillUtil.Instance.ConsumePower(player, this.skill);
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        throw new System.NotImplementedException();
    }
}

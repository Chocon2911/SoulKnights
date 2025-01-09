using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : Weapon, IAttackable
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===M4===")]
    [SerializeField] private float fireRate;
    [SerializeField] private Transform bullet;
    [SerializeField] private Skill skill;

    //==========================================Get Set===========================================
    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Equipment/Weapon/M4/M4");
        base.LoadComponents();
    }

    //========================================IAttackable=========================================
    public void Attack(Player player, int state)
    {
        if (state <= 0 || !this.skill.CanUseSkill(player)) return; 
        
        Transform newBullet = SkillUtil.Instance.Shoot(bullet, transform.position, transform.rotation);
        if (newBullet == null) return;
        
        newBullet.gameObject.SetActive(true);
        SkillUtil.Instance.ConsumeHp(player, this.skill);
        SkillUtil.Instance.ConsumeMana(player, this.skill);
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();

        M4SO m4SO = (M4SO)this.so;
        if (m4SO == null)
        {
            Debug.LogError("M4SO is null", transform.gameObject);
            return;
        }

        this.fireRate = m4SO.FireRate;
        this.bullet = m4SO.Bullet;
        this.skill.SkillCD = new Cooldown(1 / this.fireRate, Time.fixedDeltaTime);
    }
}

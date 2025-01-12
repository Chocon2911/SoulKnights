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
    [SerializeField] private bool canSkillCD;

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

    private void OnEnable()
    {
        this.canSkillCD = true;
    }

    private void FixedUpdate()
    {
        this.SkillCoolingDown();
    }

    //========================================IAttackable=========================================
    public void Attack(HpReceiver hpRecv, ManaReceiver manaRecv, int state)
    {
        if (state <= 0 
            || !this.skill.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana()) 
            || !this.skill.SkillCD.IsReady) return;
        
        Transform newBullet = SkillUtil.Instance.Shoot(bullet, transform.position, transform.rotation);
        if (newBullet == null) return;
        
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bullet == null)
        {
            Debug.LogError("Bullet is null", transform.gameObject);
            return;
        }

        bulletScript.SetShooter(transform);
        newBullet.gameObject.SetActive(true);
        SkillUtil.Instance.ConsumeHp(hpRecv, this.skill);
        SkillUtil.Instance.ConsumeMana(manaRecv, this.skill);
        this.skill.SkillCD.ResetStatus();
    }

    //===========================================Skill============================================
    private void SkillCoolingDown()
    {
        if (!this.canSkillCD || this.skill.SkillCD.IsReady) return;
        this.skill.SkillCD.CoolingDown();
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

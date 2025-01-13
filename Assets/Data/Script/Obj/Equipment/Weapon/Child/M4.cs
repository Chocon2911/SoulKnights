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
    [SerializeField] private ShootSkill shootSkill;
    [SerializeField] private Transform bullet;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Equipment/Weapon/M4/M4");
        base.LoadComponents();
    }

    private void OnEnable()
    {
        this.shootSkill.IsCharging = true;
    }

    private void FixedUpdate()
    {
        this.ShootSkillUpdate();
    }

    //========================================IAttackable=========================================
    public void Attack(HpReceiver hpRecv, ManaReceiver manaRecv, int state)
    {
        if (state <= 0) return;
        this.shootSkill.Shooting(hpRecv, manaRecv, bullet, transform, transform.position, transform.rotation);
    }

    //========================================Shoot Skill=========================================
    private void ShootSkillUpdate()
    {
        this.shootSkill.Recharging();
    }

    private void DefaultShootSkill(M4SO m4SO)
    {
        this.shootSkill = new ShootSkill(m4SO.Skill, Time.fixedDeltaTime);
        this.shootSkill.IsCharging = true;
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

        this.bullet = m4SO.Bullet;
        this.DefaultShootSkill(m4SO);
    }
}

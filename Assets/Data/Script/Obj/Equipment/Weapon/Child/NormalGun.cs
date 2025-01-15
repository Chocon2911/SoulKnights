using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : Weapon, IAttackable
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Normal Gun===")]
    [SerializeField] private ShootSkill shootSkill;
    [SerializeField] private Transform bullet;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Equipment/Weapon/NormalGun/" + transform.name);
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
        this.shootSkill.Shooting(hpRecv, manaRecv, this.bullet, transform, transform.position, transform.rotation);
    }

    //========================================Shoot Skill=========================================
    private void ShootSkillUpdate()
    {
        this.shootSkill.Recharging();
    }

    private void DefaultShootSkill(NormalGunSO normalGunSO)
    {
        this.shootSkill = new ShootSkill(normalGunSO.Skill, Time.fixedDeltaTime);
        this.shootSkill.IsCharging = true;
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();

        NormalGunSO m4SO = (NormalGunSO)this.so;
        if (m4SO == null)
        {
            Debug.LogError("NormalGunSO is null", transform.gameObject);
            return;
        }

        this.bullet = m4SO.Bullet;
        this.DefaultShootSkill(m4SO);
    }
}

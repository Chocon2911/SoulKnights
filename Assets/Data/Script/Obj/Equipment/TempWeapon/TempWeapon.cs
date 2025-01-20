using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeapon : BaseObj, ShootUser
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Weapon===")]
    [SerializeField] private float holdRad;
    [SerializeField] private WeaponUser user;
    [SerializeField] private List<TempSkill> skills;
    [SerializeField] private bool isLeft;
 
    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.skills, transform.Find("Skill"), "LoadSkills()");
        this.LoadSO(ref this.so, "SO/Equipment/Weapon/" + transform.name);

        foreach (TempSkill skill in this.skills)
        {
            skill.SetOwner(transform);
            skill.MyLoadComponents();
            skill.DefaultStat();
            skill.ResetSkill();
        }

        this.DefaultStat();
    }

    private void Update()
    {
        foreach (TempSkill skill in this.skills) skill.MyUpdate();
    }

    private void FixedUpdate()
    {
        foreach (TempSkill skill in this.skills) skill.MyFixedUpdate();
    }

    //===========================================Method===========================================
    public void Holding(Transform owner, float angle)
    {
        Vector2 pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))
            * this.holdRad;
        transform.position = pos + (Vector2)owner.position;

        float imageYAngle = this.image.transform.localEulerAngles.y;
        float imageZAngle = this.image.transform.localEulerAngles.z;
        if ((Mathf.Cos(angle * Mathf.Deg2Rad) < 0 && !this.isLeft)
            || (Mathf.Cos(angle * Mathf.Deg2Rad) > 0 && this.isLeft))
        {
            this.image.transform.localRotation = Quaternion.Euler(180, imageYAngle, imageZAngle);
            if (!this.isLeft) this.isLeft = true;
            else if (this.isLeft) this.isLeft = false;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetUser(WeaponUser user)
    {
        this.user = user;
    }

    //=========================================SkillUser==========================================
    public bool CanUseSkill(TempSkill skill)
    {
        return this.user.CanUseSkill(skill);
    }

    public void ConsumePower(TempSkill skill)
    {
        this.user.ConsumePower(skill);
    }

    //=========================================Shoot User=========================================
    public int GetSkillOrder(TempShootSkill skill)
    {
        for (int i = 0; i < this.skills.Count; i++)
        {
            if (this.skills[i].Equals(skill)) return i;
        }

        return -1;
    }

    public List<bool> CanShoot()
    {
        List<bool> result = new List<bool>();
        if (this.user.GetFirstSkillState() <= 0) result.Add(false);
        else result.Add(true);

        if (this.user.GetSecondSkillState() <= 0) result.Add(false);
        else result.Add(true);

        return result;
    }

    public Vector3 GetBulletPos()
    {
        return transform.position;
    }

    public float GetShootAngle()
    {
        return transform.eulerAngles.z;
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        WeaponSO weaponSO = (WeaponSO)this.so;
        if (weaponSO == null)
        {
            Debug.LogError("WeaponSO is null", transform.gameObject);
            return;
        }

        this.holdRad = weaponSO.HoldRad;
    }
}

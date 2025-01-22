using UnityEditor.Timeline;
using UnityEngine;

public enum RegenType
{
    INT,
    FLOAT,
}

public class TempRegenSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Regen")]
    [SerializeField] private new RegenSkillUser user;
    [SerializeField] private new RegenSkillSO so;
    [SerializeField] private Cooldown regenCD;
    [SerializeField] private int regenInt;
    [SerializeField] private float regenFloat;
    [SerializeField] private bool isRegening;

    //==========================================Get Set===========================================
    public RegenSkillUser User { get => user; set => user = value; }
    public Cooldown RegenCD { get => regenCD; set => regenCD = value; }
    public int RegenInt { get => regenInt; set => regenInt = value; }
    public float RegenFloat { get => regenFloat; set => regenFloat = value; }
    public bool IsRegening { get => isRegening; set => isRegening = value; }

    //===========================================Method===========================================
    private void Regenerate()
    {
        if (!this.regenCD.IsReady)
        {
            this.regenCD.CoolingDown();
            return;
        }

        this.RegenerateInt();
        this.RegenerateFloat();
        this.regenCD.ResetStatus();
    }

    private void FinishRegen()
    {
        this.isRegening = false;
        this.regenCD.ResetStatus();
    }
    
    private void RegenerateInt()
    {
        if (this.user.GetRegenType() != RegenType.INT) return;
        this.user.currInt() += this.regenInt;
    }

    private void RegenerateFloat()
    {
        if (this.user.GetRegenType() != RegenType.FLOAT) return;
        this.user.currFloat() += this.regenFloat;
    }

    //==========================================Override==========================================
    public override void MyLoadComponent()
    {
        base.MyLoadComponent();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
        this.LoadSO(ref this.so, "SO/Component/Other/Skill/Regen/" + this.owner.name);
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        this.isRecharging = false;
        this.isRegening = false;
        this.regenCD.ResetStatus();
    }

    public override void MyUpdate()
    {
        if (this.skillCD.IsReady
            && this.user.CanUseSkill(this)
            && this.user.CanRegen()) this.UseSkill();
    }

    public override void MyFixedUpdate()
    {
        base.MyFixedUpdate();
        if (!this.isRegening
            && this.user.CanUseSkill(this)
            && this.user.CanRegen()) this.isRecharging = true;

        if (this.isRegening) this.Regenerate();

        if ((this.user.GetRegenType() == RegenType.INT
            && this.user.currInt() >= this.user.maxInt())
            || (this.user.GetRegenType() == RegenType.FLOAT
            && this.user.currFloat() >= this.user.maxFloat())) this.FinishRegen();
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        if (this.so == null)
        {
            Debug.Log("RegenSkillSO is null", transform.gameObject);
            return;
        }

        this.regenCD = new Cooldown(this.so.RegenTime, Time.fixedDeltaTime);
        this.regenInt = this.so.RegenAmountInt;
        this.regenFloat = this.so.RegenAmountFloat;
    }

    public override void UseSkill()
    {
        this.skillCD.ResetStatus();
        this.isRecharging = false;
        this.isRegening = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DashUser : SkillUser
{
    bool CanDash();
    Vector2 GetDashDir();
    Rigidbody2D GetRb();
    void OnDashing();
}

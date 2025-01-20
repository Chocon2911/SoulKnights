using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : Movement
{
    //==========================================Override==========================================
    protected override void Move()
    {
        MovementUtil.Instance.MoveForward(this.user.GetRb(), this.moveSpeed);
    }
}

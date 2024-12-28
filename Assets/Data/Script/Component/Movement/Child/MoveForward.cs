using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : BaseMovement
{
    //==========================================Override==========================================
    protected override Vector2 GetDir()
    {
        float xDir = Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad);
        float yDir = Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad);

        return new Vector2(xDir, yDir);
    }
}

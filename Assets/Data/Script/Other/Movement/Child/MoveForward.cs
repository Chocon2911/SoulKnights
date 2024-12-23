using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : BaseMovement
{
    //========================================Constructor=========================================
    public MoveForward(Rigidbody2D rb, float moveSpeed, float angle)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.GetDir(angle);
    }

    //===========================================Method===========================================
    private void GetDir(float angle)
    {
        float rotX = Mathf.Cos(angle);
        float rotY = Mathf.Sin(angle);

        this.moveDir = new Vector2(rotX, rotY).normalized;
    }
}

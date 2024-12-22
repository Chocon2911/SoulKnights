using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoveByKeyboard : BaseMovement
{
    //========================================Constructor=========================================
    public MoveByKeyboard(Rigidbody2D rb, float moveSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    //===========================================Method===========================================
    private void UpdateDir()
    {
        this.moveDir = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A)) { this.moveDir.x = -1; }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D)) this.moveDir.x = 1;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S)) this.moveDir.y = -1;
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W)) this.moveDir.y = 1;
    }

    //==========================================Override==========================================
    public override void Move()
    {
        this.UpdateDir();
        base.Move();
    }
}

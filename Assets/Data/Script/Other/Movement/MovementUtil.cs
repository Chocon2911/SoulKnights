using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtil
{
    //==========================================Variable==========================================
    private static MovementUtil instance;

    //==========================================Get Set===========================================
    public static MovementUtil Instance
    {
        get
        {
            if (instance == null) instance = new MovementUtil();
            return instance;
        }
    }

    //========================================Constructor=========================================
    public MovementUtil()
    {
        if (instance != null)
        {
            Debug.LogError("One MovementUtil Only");
            return;
        }

        instance = this;
    }

    //===========================================Method===========================================
    public void Move(Rigidbody2D rb, float moveSpeed, Vector2 moveDir)
    {
        if (rb == null)
        {
            Debug.LogError("Rb is null");
            return;
        }

        rb.velocity += moveSpeed * moveDir;
    }

    public void MoveByKeyboard(Rigidbody2D rb, float moveSpeed)
    {
        Vector2 moveDir = InputManager.Instance.MoveDir.normalized;
        this.Move(rb, moveSpeed, moveDir);
    }

    public void ChaseTarget(Rigidbody2D rb, float moveSpeed, Transform target) 
    {
        Vector2 moveDir = (target.position - rb.transform.position).normalized;
        this.Move(rb, moveSpeed, moveDir);
    }

    public void MoveForward(Rigidbody2D rb, float moveSpeed)
    {
        float xDir = Mathf.Cos(rb.transform.eulerAngles.z * Mathf.Deg2Rad);
        float yDir = Mathf.Sin(rb.transform.eulerAngles.z * Mathf.Deg2Rad);

        Vector2 moveDir = new Vector2(xDir, yDir).normalized;
        this.Move(rb, moveSpeed, moveDir);
    }
}

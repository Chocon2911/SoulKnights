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

    //============================================Move============================================
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

    public void MoveForward(Rigidbody2D rb, float moveSpeed)
    {
        float xDir = Mathf.Cos(rb.transform.eulerAngles.z * Mathf.Deg2Rad);
        float yDir = Mathf.Sin(rb.transform.eulerAngles.z * Mathf.Deg2Rad);

        Vector2 moveDir = new Vector2(xDir, yDir).normalized;
        this.Move(rb, moveSpeed, moveDir);
    }

    //========================================Chase Target========================================
    public void ChaseTarget(Rigidbody2D rb, float moveSpeed, Transform target)
    {
        Vector2 moveDir = (target.position - rb.transform.position).normalized;
        this.Move(rb, moveSpeed, moveDir);
    }

    public void ChaseTargetWithRot(Rigidbody2D rb, float rotSpeed, float moveSpeed, Transform target, Transform myObj) 
    {
        float xDistance = target.position.x - myObj.position.x;
        float yDistance = target.position.y - myObj.position.y;

        float angle = Mathf.Atan2(yDistance, xDistance) * Mathf.Rad2Deg;
        float myObjAngle = myObj.eulerAngles.z;

        float angleDiff = angle - myObjAngle;
        if (angleDiff > 0) this.Rotate(myObj, rotSpeed);
        else if (angleDiff < 0) this.Rotate(myObj, -rotSpeed);

        this.MoveForward(rb, moveSpeed);
    }

    private void Rotate(Transform myObj, float addAngle) 
    {
        myObj.eulerAngles = new Vector3(0, 0, myObj.eulerAngles.z + addAngle);
    }
}

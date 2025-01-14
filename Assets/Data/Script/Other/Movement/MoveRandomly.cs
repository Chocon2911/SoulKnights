using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomly
{
    //==========================================Variable==========================================
    [SerializeField] private Vector2 goalPos;
    [SerializeField] private float randomRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMove;
    [SerializeField] private bool isReachGoal;

    //==========================================Get Set===========================================
    public float RandomRange { get => randomRange; set => randomRange = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    //========================================Constructor=========================================
    public MoveRandomly(float randomRange, float moveSpeed)
    {
        this.randomRange = randomRange;
        this.moveSpeed = moveSpeed;
        this.isMove = false;
        this.isReachGoal = false;
    }

    //===========================================Method===========================================
    public void StartMove(Vector2 ownerPos)
    {
        this.isMove = true;
        this.isReachGoal = false;
        this.GetRandomGoalPos(ownerPos);    
    }
    
    public void Moving(Rigidbody2D rb, float moveSpeed)
    {
        if (!this.isMove) return;
        Vector2 moveDir = (goalPos - (Vector2)rb.transform.position).normalized;
        MovementUtil.Instance.Move(rb, moveSpeed, moveDir);
        this.CheckReachGoal(rb.transform.position);

        if (!this.isReachGoal) return;
        this.FinishMove();
    }

    public void FinishMove()
    {
        this.isMove = false;
    }

    private void CheckReachGoal(Vector2 ownerPos)
    {
        if (Vector2.Distance(ownerPos, this.goalPos) < 0.1f) this.isReachGoal = true;
    }

    private void GetRandomGoalPos(Vector2 ownerPos)
    {
        this.goalPos = Util.Instance.RandomPos(ownerPos, randomRange);
    }
} 

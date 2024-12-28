using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerComponent
{
    //==========================================Variable==========================================
    [SerializeField] private MoveByKeyboard moveByKeyBoard;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.moveByKeyBoard, transform, "LoadMoveByKeyBoard()");
    }

    //===========================================Method===========================================
    public void Handling()
    {
        this.player.Rb.velocity = Vector2.zero;
        this.moveByKeyBoard.Move();
    }

    //==========================================Override==========================================
    public override void DefaultStat()
    {
        base.DefaultStat();
        // MoveByKeyBoard
        this.moveByKeyBoard.Rb = this.player.Rb;
        this.moveByKeyBoard.MoveSpeed = this.player.MoveSpeed;
    }
}

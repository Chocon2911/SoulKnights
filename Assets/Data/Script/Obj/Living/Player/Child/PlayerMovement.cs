using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerObjChild
{
    //==========================================Variable==========================================
    [Header("Player Movement")]
    [SerializeField] private BaseMovement movement;

    //===========================================Unity============================================
    private void Update()
    {
        this.movement.Move();
    }

    //==========================================Override==========================================
    public override void DefaultStat()
    {
        this.movement = new MoveByKeyboard(this.Manager.Rb, this.Manager.MoveSpeed);
    }
}

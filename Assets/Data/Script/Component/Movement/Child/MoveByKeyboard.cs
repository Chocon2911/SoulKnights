using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKeyboard : BaseMovement
{
    //==========================================Override==========================================
    protected override Vector2 GetDir()
    {
        return InputManager.Instance.MoveDir;
    }
}

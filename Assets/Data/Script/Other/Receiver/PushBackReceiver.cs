using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PushBackReceiver
{
    public void ReceiveForce(Vector2 force, float pushBackDuration);
}

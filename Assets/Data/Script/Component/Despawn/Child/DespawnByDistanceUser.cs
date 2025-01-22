using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DespawnByDistanceUser : DespawnUser
{
    Vector2 GetTargetPos();
}

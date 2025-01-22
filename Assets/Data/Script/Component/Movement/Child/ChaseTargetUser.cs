using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ChaseTargetUser : MovementUser
{
    Transform GetTarget();
}

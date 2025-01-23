using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ChargeOnMoving
{
    void ActivateCharge();
    void OnCharging(float totalTime, float currTime);
    void FinishCharge();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Chargable
{
    void ActivateCharge();
    void OnCharging();
    void FinishCharge();
}

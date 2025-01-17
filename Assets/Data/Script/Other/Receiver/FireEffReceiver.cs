using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FireEffReceiver : DamageReceiver
{
    public void ReceiveFireEff(float fireDuration, int damage);
}

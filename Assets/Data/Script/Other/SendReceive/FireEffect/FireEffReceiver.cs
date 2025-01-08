using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FireEffReceiver : DamageReceiver
{
    public void Receive(float fireDuration, float damage);
}

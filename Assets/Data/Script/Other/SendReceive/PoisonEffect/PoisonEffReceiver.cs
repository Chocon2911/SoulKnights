using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoisonEffReceiver : DamageReceiver
{
    public void ReceivePoisonEff(float poisonDuration, int damage);
}

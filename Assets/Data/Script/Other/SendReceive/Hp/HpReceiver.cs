using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HpReceiver : DamageReceiver, HpHolder
{
    public void ReceiveHp(int hp);
}

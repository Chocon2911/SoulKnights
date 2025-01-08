using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HpReceiver : DamageReceiver
{
    public int GetCurrHp();
    public void Receive(int hp);
}

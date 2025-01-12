using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public void Attack(HpReceiver hpRecv, ManaReceiver manaRecv, int state);
}

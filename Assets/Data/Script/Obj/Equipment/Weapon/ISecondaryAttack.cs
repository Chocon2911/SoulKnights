using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISecondaryAttack 
{
    public void SecondaryAttack(HpReceiver hpRecv, ManaReceiver manaRecv, int state);
}

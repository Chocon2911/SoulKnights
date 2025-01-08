using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ManaReceiver
{
    public int GetCurrMana();
    public void Receive(int mana);
}

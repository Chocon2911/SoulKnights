using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ManaReceiver : ManaHolder
{
    public void Receive(int mana);
}

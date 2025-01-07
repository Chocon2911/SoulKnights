using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSender : Sender
{
    //==========================================Variable==========================================
    private int mana;

    //==========================================Get Set===========================================
    public int Mana
    {
        get => mana;
        set => mana = value;
    }

    //===========================================Method===========================================
    public void Send(ManaReceiver receiver)
    {
        receiver.Receive(this.mana);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaReceiver : Receiver
{
    //==========================================Variable==========================================
    [SerializeField] private int receivedMana;

    //==========================================Get Set===========================================
    public int ReceivedMana
    {
        get => receivedMana;
        set => receivedMana = value;
    }

    //===========================================Method===========================================
    public void Receive(int mana)
    {
        this.receivedMana = mana;
        this.OnReceive();
    }
}

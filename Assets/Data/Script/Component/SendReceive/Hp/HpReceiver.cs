using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpReceiver : Receiver
{
    //==========================================Variable==========================================
    [SerializeField] private int receivedHp;

    //==========================================Get Set===========================================
    public int ReceivedHp
    {
        get => receivedHp;
        set => receivedHp = value;
    }

    //===========================================Method===========================================
    public void Receive(int hp)
    {
        this.ReceivedHp = hp;
        this.OnReceive();
    }
}

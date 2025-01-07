using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSender : Sender
{
    //==========================================Variable==========================================
    private int hp;

    //==========================================Get Set===========================================
    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    //===========================================Method===========================================
    public void Send(HpReceiver receiver)
    {
        receiver.Receive(this.hp);
    }
}

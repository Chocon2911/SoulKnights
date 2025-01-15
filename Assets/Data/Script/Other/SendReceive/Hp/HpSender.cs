using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HpSender
{
    public void SendHp(HpReceiver receiver);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoisonEffSender
{
    public void Send(PoisonEffReceiver receiver);
}

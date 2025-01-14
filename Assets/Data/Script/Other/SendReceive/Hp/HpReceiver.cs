using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HpReceiver : DamageReceiver
{
<<<<<<< Updated upstream
    public int GetCurrHp();
    public void Receive(int hp);
=======
    public void ReceiveHp(int hp);
>>>>>>> Stashed changes
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
<<<<<<< Updated upstream
    public void Attack(Player player, int state);
=======
    public void Attack(HpReceiver hpRecv, ManaReceiver manaRecv, int state);

    public void Attack(WeaponUser weaponUser, int state);
>>>>>>> Stashed changes
}

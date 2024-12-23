using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargable
{
    public int GetState();
    public IEnumerator Charging();
    public void ReCharge();
}

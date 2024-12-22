using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargable
{
    public int GetState();
    public void Charging();
    public void ReCharge();
}

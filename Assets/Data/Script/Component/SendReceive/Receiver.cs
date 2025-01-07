using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Receiver : HuyMonoBehaviour
{
    public System.Action OnReceive = () => { };
}

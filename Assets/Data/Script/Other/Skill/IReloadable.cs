using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadable
{
    public bool IsReload();
    public void Reload();
    public void Reloading();
}

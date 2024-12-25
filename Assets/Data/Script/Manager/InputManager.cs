using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    private static InputManager instance;

    //===Input===
    [SerializeField] private KeyCode leftMove = KeyCode.A;
    [SerializeField] private KeyCode rightMove = KeyCode.D;
    [SerializeField] private KeyCode frontMove = KeyCode.W;
    [SerializeField] private KeyCode backMove = KeyCode.S;

    [SerializeField] private KeyCode shift = KeyCode.LeftShift;
    [SerializeField] private KeyCode space = KeyCode.Space;

    [SerializeField] private KeyCode hotBar1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode hotBar2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode hotBar3 = KeyCode.Alpha3;
    [SerializeField] private KeyCode hotBar4 = KeyCode.Alpha4;
    [SerializeField] private KeyCode hotBar5 = KeyCode.Alpha5;
    [SerializeField] private KeyCode hotBar6 = KeyCode.Alpha6;
    [SerializeField] private KeyCode hotBar7 = KeyCode.Alpha7;
    [SerializeField] private KeyCode hotBar8 = KeyCode.Alpha8;
    [SerializeField] private KeyCode hotBar9 = KeyCode.Alpha9;

    [SerializeField] private KeyCode leftMouse = KeyCode.Mouse0;
    [SerializeField] private KeyCode rightMouse = KeyCode.Mouse1;

    //==========================================Get Set===========================================
    public static InputManager Instace => instance;

    //===Input===
    public KeyCode LeftMove => leftMove;
    public KeyCode RightMove => rightMove;
    public KeyCode FrontMove => frontMove;
    public KeyCode BackMove => backMove;


    public KeyCode Shift => shift;
    public KeyCode Space => space;


    public KeyCode HotBar1 => hotBar1;
    public KeyCode HotBar2 => hotBar2;
    public KeyCode HotBar3 => hotBar3;
    public KeyCode HotBar4 => hotBar4;
    public KeyCode HotBar5 => hotBar5;
    public KeyCode HotBar6 => hotBar6;
    public KeyCode HotBar7 => hotBar7;
    public KeyCode HotBar8 => hotBar8;
    public KeyCode HotBar9 => hotBar9;


    public KeyCode LeftMouse => leftMouse;
    public KeyCode RightClick => rightMouse;

    //===========================================Unity============================================
    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("instance not null", transform.gameObject);
            return;
        }

        instance = this;
        base.Awake();
    }
}

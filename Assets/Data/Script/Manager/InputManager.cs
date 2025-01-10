using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    private static InputManager instance;

    [Header("Input")]
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

    [Header("Stat")]
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private int leftClickState;
    [SerializeField] private int rightClickState;
    [SerializeField] private int shiftState;
    [SerializeField] private int spaceState;
    [SerializeField] private Vector2 mousePos;

    //==========================================Get Set===========================================
    public static InputManager Instance => instance;

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

    //===Stat===
    public Vector2 MoveDir => moveDir;
    public int LeftClickState => leftClickState;
    public int RightClickState => rightClickState;
    public int ShiftState => shiftState;
    public int SpaceState => spaceState;
    public Vector2 MousePos => mousePos;

    //===========================================Unity============================================
    private void Update()
    {
        this.handleInputU();
    }

    private void FixedUpdate()
    {
        this.handleInputFU();
    }

    //===========================================Method===========================================
    private void handleInputFU()
    {
        this.moveDir = Vector2.zero;
        this.leftClickState = 0;
        this.rightClickState = 0;
        this.shiftState = 0;
        this.spaceState = 0;

        if (Input.GetKey(this.leftMouse)) this.leftClickState = 2;
        if (Input.GetKey(this.rightMouse)) this.rightClickState = 2;
        if (Input.GetKey(this.shift)) this.shiftState = 2;
        if (Input.GetKey(this.space)) this.spaceState = 2;
    }
    
    private void handleInputU()
    {
        //===Handle===
        //MoveDir
        if (Input.GetKeyDown(this.rightMove) || Input.GetKey(this.rightMove)) this.moveDir.x = 1;
        else if (Input.GetKeyDown(this.leftMove) || Input.GetKey(this.leftMove)) this.moveDir.x = -1;

        if (Input.GetKeyDown(this.backMove) || Input.GetKey(this.backMove)) this.moveDir.y = -1;
        else if (Input.GetKeyDown(this.frontMove) || Input.GetKey(this.frontMove)) this.moveDir.y = 1;

        //State
        if (Input.GetKeyDown(this.leftMouse)) this.leftClickState = 1;
        else if (Input.GetKey(this.leftMouse)) this.leftClickState = 2;

        if (Input.GetKeyDown(this.rightMouse)) this.rightClickState = 1;
        else if (Input.GetKey(this.rightMouse)) this.rightClickState = 2;
        
        if (Input.GetKeyDown(this.shift)) this.shiftState = 1;
        else if (Input.GetKey(this.shift)) this.shiftState = 2;

        if (Input.GetKeyDown(this.space)) this.spaceState = 1;
        else if (Input.GetKey(this.space)) this.spaceState = 2;

        // MousePos
        this.mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

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

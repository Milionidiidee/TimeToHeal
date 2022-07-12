using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    PlayerInputs _input;

    //States
    PlayerBaseState _currentState;
    IdleState _idleState;
    RunState _runState;

    public PlayerBaseState PlayerCurrentState { get { return _currentState; } set { _currentState = value; } }
    public IdleState PlayerIdleState { get { return _idleState; } set { _idleState = value; } }
    public RunState PlayerRunState { get { return _runState; } set { _runState = value; } }

    //Aim and Fire Variables
    Vector3 _aimPoint;
    float _requestFire = 0;                                         //It's <0.5 if we don't want to fire

    public Vector3 AimPoint { get { return _aimPoint; } }

    [Header("Controls")]
    [Range(5f,20f)]
    [SerializeField] [Tooltip("Maximum distance a player can aim to")]
    float _maxGamepadAimDistance = 20f;

    //Move Variables
    Vector2 _desiredMoveDirection = Vector2.zero;
    bool _isMoving = false;
    Rigidbody2D _rigidBody;

    [Header("Movement Variables")]
    [Range(2f, 20f)]
    [SerializeField] float _moveSpeed = 10f;
    [Range(2f, 20f)]
    [SerializeField] float _accelleration = 12f;
    [Range(2f, 20f)]
    [SerializeField] float _decelleration = 8f;

    public bool IsMoving { get { return _isMoving; } }
    public Vector2 DesiredMoveDirection { get { return _desiredMoveDirection; } }
    public Rigidbody2D PlayerRigidbody2D { get { return _rigidBody; } }

    public float MoveSpeed { get { return _moveSpeed; } }
    public float Accelleration { get { return _accelleration; } }
    public float Decelleration { get { return _decelleration; } }

    void Awake()
    {

        _rigidBody = GetComponent<Rigidbody2D>();

        //Input Management System
        //When an event is triggered the input is set in the correct variables to be then executed
        _input = new PlayerInputs();

        _input.PlayerControls.Run.performed += ctx =>
        {
            _desiredMoveDirection = ctx.ReadValue<Vector2>().normalized;
            if (_desiredMoveDirection.magnitude > .1f) _isMoving = true;
            else _isMoving = false;
        };
        _input.PlayerControls.Run.canceled += ctx =>
        {
            _desiredMoveDirection = Vector2.zero;
            _isMoving = false;
        };
        _input.PlayerControls.Fire.performed += ctx => _requestFire = ctx.ReadValue<float>();
        _input.PlayerControls.Aim.performed += ctx => {
            if (ctx.control.device.name == "Mouse")
            {
                //Assuming the input comes from the mouse position
                Vector2 screenPoint = ctx.ReadValue<Vector2>();
                _aimPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, Camera.main.nearClipPlane));
                _aimPoint.z = 0;
            }
            else
            {
                //The only other supported device currently is a GamePad
                Vector2 aimPos = ctx.ReadValue<Vector2>();
                _aimPoint = new Vector3(transform.position.x + aimPos.x * _maxGamepadAimDistance,
                                            transform.position.y + aimPos.y * _maxGamepadAimDistance,
                                            0);
            }
        };

        //State starting function
        _idleState = new IdleState(this);
        _runState = new RunState(this);
    }

    void OnEnable()
    {
        _input.PlayerControls.Enable();
    }

    void OnDisable()
    {
        _input.PlayerControls.Disable();
    }

    void Start()
    {
        _currentState = _idleState;
        _currentState.EnterState();
    }
    
    void Update()
    {

    }

    void FixedUpdate()
    {
        _currentState.FixedUpdateState();
    }

#if UNITY_EDITOR
    //Code to make the editor more intuitive for the level designer
    void OnDrawGizmosSelected()
    { 
        Gizmos.color = Color.green;
        Handles.color = Color.green;

        Handles.DrawWireDisc(transform.position, transform.forward, _maxGamepadAimDistance);
        Handles.DrawSolidDisc(_aimPoint, transform.forward, .1f);

        Handles.color = Color.white;
        Gizmos.color = Color.white;
    }
#endif
}

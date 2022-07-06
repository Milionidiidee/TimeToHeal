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

    //Aim and Fire Variables
    Vector3 _aimPoint;
    float _requestFire = 0;                                         //It's <0.5 if we don't want to fire

    [Range(5f,20f)]
    [SerializeField] [Tooltip("Maximum distance a player can aim to")]
    float _maxGamepadAimDistance = 20f;

    //Move Variables
    Vector2 _desiredMoveDirection = Vector2.zero;

    void Awake()
    {
        //Input Management System
        //When an event is triggered the input is set in the correct variables to be then executed
        _input = new PlayerInputs();

        _input.PlayerControls.Run.performed += ctx => _desiredMoveDirection = ctx.ReadValue<Vector2>().normalized;
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
        
    }
    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
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

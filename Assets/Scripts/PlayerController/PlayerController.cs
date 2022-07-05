using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputs _input;

    //Aim and Fire Variables
    Vector3 _aimPoint;
    float _requestFire = 0;                                        //It's <0.5 if we don't want to fire

    //Move Variables
    Vector2 _desiredMoveDirection = Vector2.zero;

    void Awake()
    {
        _input = new PlayerInputs();

        _input.PlayerControls.Run.performed += ctx => _desiredMoveDirection = ctx.ReadValue<Vector2>();
        _input.PlayerControls.Fire.performed += ctx => _requestFire = ctx.ReadValue<float>();
        _input.PlayerControls.Aim.performed += ctx => {
            //Assuming the input comes from the mouse position
            Vector2 screenPoint = ctx.ReadValue<Vector2>();
            _aimPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, Camera.main.nearClipPlane));
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
        Debug.Log(_requestFire);
    }

    void FixedUpdate()
    {
        
    }
}

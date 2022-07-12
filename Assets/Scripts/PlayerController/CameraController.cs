using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    /*
     
        We want the camera to let us see in the direction of aiming

     */

    [SerializeField] float _distanceFromPlayerToCameraCenter = 10f;
    [SerializeField] float _cameraResponsivness = 5f;

    PlayerController _playerController;
    Camera _camera;

    float _zDepth = -10;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 aimDirection = _playerController.AimPoint.normalized;

        Vector3 targetPosition = new Vector3(transform.position.x + aimDirection.x * _distanceFromPlayerToCameraCenter,
            transform.position.y + aimDirection.y * _distanceFromPlayerToCameraCenter,
            _zDepth);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, Time.unscaledDeltaTime * _cameraResponsivness);
    }
}

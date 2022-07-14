using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSystem : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] float _projectileSpeed = 4f;
    [SerializeField] float _firingCoolDown = .3f;

    PlayerController _playerController;
    float _lastFire = 0;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _lastFire = Time.time;
    }

    private void Update()
    {
        if (_playerController.RequestFire && Time.time - _lastFire > _firingCoolDown)
        {
            GameObject go = Instantiate(_projectile, transform.position + _playerController.AimPoint.normalized, Quaternion.identity);
            go.GetComponent<ProjectileController>().Speed = Vector3.Normalize(_playerController.AimPoint - transform.position) * _projectileSpeed;
            _lastFire = Time.time;
        }    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    /*
     
        Health is decreased if you are stationary for more than _timeBetweenHealthDecrease, you can increase health by killing enemies
     
     */


    [SerializeField] float _timeBetweenHealthDecrease = 5f;
    [SerializeField] int _maxHealth = 10;

    PlayerController _playerController;

    int _health;
    bool _isTimerRunning = false;
    bool _isDead = false;
    float _timeSinceLastHealthDecrease = 0;

    void Start()
    {
        _health = _maxHealth;
        _timeSinceLastHealthDecrease = 0;
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_isTimerRunning && !_isDead)
        {
            _timeSinceLastHealthDecrease += Time.unscaledDeltaTime;
            if (_timeSinceLastHealthDecrease > _timeBetweenHealthDecrease)
            {
                _health -= 1;
                ResetTimer();
            }
        }
        if (_health <= 0 && !_isDead)
            Die();
    }


    public void IncreaseHealth(int _increasedHealth)
    {
        if (!_isDead)
            _health += _increasedHealth;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void DecreaseHealth(int _decreasedHealth)
    {
        if (!_isDead)
            _health -= _decreasedHealth;
        if (_health <= 0)
            Die();
    }

    public void Resuscitate()
    {
        _isDead = false;
        _timeSinceLastHealthDecrease = 0;
        _health = _maxHealth;
    }
    public void Die()
    {
        _isDead = true;
        _health = 0;
    }

    public void StartTimer()
    {
        _isTimerRunning = true;
    }
    public void StopTimer()
    {
        ResetTimer();
        _isTimerRunning = false;
    }
    public void ResetTimer()
    {
        _timeSinceLastHealthDecrease = 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    PlayerHealth _playerHealth;

    public IdleState(PlayerController context) : base(context)
    {
    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsMoving)
            SwitchState(_ctx.PlayerRunState);
    }

    public override void EnterState()
    {
        _playerHealth = _ctx.GetComponent<PlayerHealth>();
        _playerHealth.StartTimer();
    }

    public override void ExitState()
    {
        _playerHealth.StopTimer();
    }

    public override void FixedUpdateState()
    {
        CheckSwitchState();
    }
}

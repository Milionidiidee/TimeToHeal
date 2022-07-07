using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerController _ctx;
    public PlayerBaseState(PlayerController context) {
        _ctx = context;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void FixedUpdateState();
    public abstract void CheckSwitchState();
    protected void SwitchState(PlayerBaseState newState)
    {
        _ctx.PlayerCurrentState.ExitState();
        _ctx.PlayerCurrentState = newState;
        _ctx.PlayerCurrentState.EnterState();
    }
}

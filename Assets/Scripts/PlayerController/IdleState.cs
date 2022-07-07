using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
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
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState()
    {
        CheckSwitchState();
    }
}

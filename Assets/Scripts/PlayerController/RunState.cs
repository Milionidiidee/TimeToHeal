using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public RunState(PlayerController context) : base(context)
    {
    }

    public override void CheckSwitchState()
    {
        if (!_ctx.IsMoving && _ctx.PlayerRigidbody2D.velocity.magnitude < .1f)
            SwitchState(_ctx.PlayerIdleState);
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState()
    {
        //min 1.53 https://www.youtube.com/watch?v=KbtcEVCM7bw&t=113s
        //movement calculation explained
        Vector2 targetSpeed = _ctx.DesiredMoveDirection * _ctx.MoveSpeed;
        Vector2 speedDiff = targetSpeed - _ctx.PlayerRigidbody2D.velocity;
        float accellRate = Vector2.SqrMagnitude(targetSpeed) > .1f ? _ctx.Accelleration : _ctx.Decelleration;
        Vector2 movement = speedDiff * accellRate;

        _ctx.PlayerRigidbody2D.AddForce(movement);

        CheckSwitchState();
    }
}

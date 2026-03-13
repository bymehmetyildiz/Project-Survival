using UnityEngine;

public class ArmedMoveState : PlayerState
{
    public ArmedMoveState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StopAllCoroutines();
        player.pistolAimRig.weight = 0.0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();        
        if (player.IsMoving() == false)
            stateMachine.ChangeState(player.aimState);

        player.cc.Move(player.Movement() * Time.deltaTime);
    }
}

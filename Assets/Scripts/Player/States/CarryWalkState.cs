using UnityEngine;

public class CarryWalkState : PlayerState
{
    public CarryWalkState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.cc.Move(player.Movement() * Time.deltaTime);

        if (player.IsMoving() == false && player.isBusyCarrying)
            stateMachine.ChangeState(player.carryIdleState);
    }
}

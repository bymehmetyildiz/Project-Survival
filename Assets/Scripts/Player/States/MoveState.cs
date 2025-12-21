using UnityEngine;

public class MoveState : PlayerState
{
    public MoveState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
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

        if(player.IsMoving() == false)
            stateMachine.ChangeState(player.idleState);
        else if (player.CanInteractWitResource())
            stateMachine.ChangeState(player.workState);
    }
}

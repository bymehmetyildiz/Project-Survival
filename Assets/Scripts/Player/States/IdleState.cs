using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
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

        player.ApplyGravity();

        if(player.IsMoving())        
            stateMachine.ChangeState(player.moveState);
        else if (player.CanInteractWitResource())
            stateMachine.ChangeState(player.workState);
        else if(player.CanCollectResource())
            stateMachine.ChangeState(player.pickUpState);
    }
}

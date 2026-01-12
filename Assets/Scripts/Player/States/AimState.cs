using UnityEngine;

public class AimState : PlayerState
{
    public AimState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.weapon.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangeState(player.idleState);
            player.weapon.SetActive(false);
            return;
        }

        player.ApplyGravity();

        if (player.IsMoving())
            stateMachine.ChangeState(player.armedMoveState);


    }
}

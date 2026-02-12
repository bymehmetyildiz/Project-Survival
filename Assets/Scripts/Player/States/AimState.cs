using UnityEngine;

public class AimState : PlayerState
{
    public AimState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.animator.SetInteger("AimIndex", player.aimIndex);
        player.weapon.SetActive(true);       

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.ApplyGravity();

        if (player.IsMoving())
        {
            stateMachine.ChangeState(player.armedMoveState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangeState(player.idleState);
            player.weapon.SetActive(false);
            return;
        }

    }
}

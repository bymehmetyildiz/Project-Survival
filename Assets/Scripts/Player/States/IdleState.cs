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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.aimIndex = 0.0f;
            stateMachine.ChangeState(player.drawWeaponState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.aimIndex = 0.5f;
            stateMachine.ChangeState(player.drawWeaponState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.aimIndex = 1.0f;
            stateMachine.ChangeState(player.drawWeaponState);
        }

    }
}

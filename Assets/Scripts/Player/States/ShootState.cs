using UnityEngine;

public class ShootState : PlayerState
{
    public ShootState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.aimIndex == 0.0f)
            player.pistolAimRig.weight = 1.0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.currentWeapon.weaponType == WeaponType.PISTOL)
        {
            if (Input.GetMouseButtonDown(0))
            {
                triggerCalled = false;
                player.animator.Play("Shoot", 0, 0f);
                return;
            }
            else if (player.IsMoving())
            {
                stateMachine.ChangeState(player.armedMoveState);
                return;
            }
            else if (triggerCalled)
            {
                stateMachine.ChangeState(player.aimState);
            }            
        }
        else if (player.currentWeapon.weaponType == WeaponType.RIFLE)
        {
            if (Input.GetMouseButtonUp(0))
                stateMachine.ChangeState(player.aimState);
        }
        
        else        
        {
            if (triggerCalled)
                stateMachine.ChangeState(player.aimState);
        }
        
    }
}

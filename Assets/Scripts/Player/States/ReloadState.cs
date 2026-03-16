using UnityEngine;

public class ReloadState : PlayerState
{
    public ReloadState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.pistolAimRig.weight = 0.0f;
    }

    public override void Exit()
    {
        base.Exit();
        if(player.currentWeapon.weaponType == WeaponType.HEAVY && player.currentWeapon.currentAmmo <= 0)        
            player.currentWeapon.rocket.SetActive(false);
        
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            player.currentWeapon.Reload();
            stateMachine.ChangeState(player.aimState);

        }
        else if (player.IsMoving())
            stateMachine.ChangeState(player.armedMoveState);
    }
}

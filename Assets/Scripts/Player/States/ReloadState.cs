using UnityEngine;

public class ReloadState : PlayerState
{
    private bool reloadFinished;

    public ReloadState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player)
        : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        reloadFinished = false;
        player.pistolLHandRig.weight = 0.0f;
    }

    public override void Update()
    {
        base.Update();
        player.ApplyGravity();

        if (reloadFinished)
        {
            reloadFinished = false;
            player.currentWeapon.Reload();
            stateMachine.ChangeState(player.aimState);
        }
    }

    public void OnReloadFinished()
    {
        reloadFinished = true;
    }

    public override void Exit()
    {
        base.Exit();
        if(player.currentWeapon.weaponType == WeaponType.HEAVY && player.currentWeapon.currentAmmo <= 0)        
            player.currentWeapon.rocket.SetActive(false);
        
    }
}

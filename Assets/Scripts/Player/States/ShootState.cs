using UnityEngine;

public class ShootState : PlayerState
{
    public ShootState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        triggerCalled = false;

        if (player.aimIndex == 0.0f)
            player.pistolLHandRig.weight = 1.0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.ClosestEnemy() != null)
        {
            Vector3 direction = (player.ClosestEnemy().transform.position - player.transform.position).normalized;
            direction.y = 0; // prevent tilting up/down

            Quaternion rot = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, player.turnSpeed * Time.deltaTime);           
        }

        if (player.currentWeapon.weaponType == WeaponType.PISTOL)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(player.currentWeapon.currentAmmo <= 0 && player.currentWeapon.bulletAmount > 0)
                {
                    stateMachine.ChangeState(player.reloadState);
                    return;
                }
                else if(player.currentWeapon.currentAmmo <= 0 && player.currentWeapon.bulletAmount <= 0)
                {
                    stateMachine.ChangeState(player.aimState);
                    return;
                }
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
            if (player.currentWeapon.currentAmmo <= 0)
            {
                if (player.currentWeapon.bulletAmount > 0)
                {
                    stateMachine.ChangeState(player.reloadState);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(player.aimState);
                    return;
                }
            }

            if (triggerCalled && Input.GetMouseButton(0))
            {
                player.animator.Play("Shoot", 0, 0f);
                triggerCalled = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                stateMachine.ChangeState(player.aimState);
                return;
            }
        }
        else        
        {
            if (triggerCalled)
                stateMachine.ChangeState(player.aimState);
        }

        if(player.currentWeapon.currentAmmo <= 0 && player.currentWeapon.bulletAmount > 0)        
            stateMachine.ChangeState(player.reloadState);
    }
}

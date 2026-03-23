using UnityEngine;

public class AimState : PlayerState
{
    public AimState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter(); 
        player.animator.SetFloat("AimIndex", player.aimIndex);
        player.UpdateCurrentWeapon();
        
    }

    public override void Exit()
    {
        base.Exit();
        player.pistolLHandRig.weight = 0.0f;        
    }

    public override void Update()
    {
        base.Update();

        player.ApplyGravity();

        if (player.ClosestEnemy() != null)
        {
            Vector3 direction = (player.ClosestEnemy().transform.position - player.transform.position).normalized;            
            direction.y = 0; // prevent tilting up/down

            Quaternion rot = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, player.turnSpeed * Time.deltaTime);
           
        }
        

        if (Input.GetMouseButtonDown(0) && player.currentWeapon.currentAmmo > 0)
            stateMachine.ChangeState(player.shootState);
        else if (player.currentWeapon.currentAmmo <= 0 && player.currentWeapon.bulletAmount > 0)
            stateMachine.ChangeState(player.reloadState);

        if (player.IsMoving())
        {
            stateMachine.ChangeState(player.armedMoveState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangeState(player.idleState);
            player.currentWeapon.gameObject.SetActive(false);
            player.currentWeapon = null;
            player.StopAllCoroutines();
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(player.aimIndex != 0.0f)
            {
                player.aimIndex = 0.0f;
                player.currentWeapon.gameObject.SetActive(false);
                player.currentWeapon = null;
                stateMachine.ChangeState(player.drawWeaponState);
             
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (player.aimIndex != 0.5f)
            {
                player.aimIndex = 0.5f;
                player.currentWeapon.gameObject.SetActive(false);
                player.currentWeapon = null;
                stateMachine.ChangeState(player.drawWeaponState);
               
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (player.aimIndex != 1.0f)
            {
                player.aimIndex = 1.0f;
                player.currentWeapon.gameObject.SetActive(false);
                player.currentWeapon = null;
                stateMachine.ChangeState(player.drawWeaponState);
             
            }
        }

        


    }
}

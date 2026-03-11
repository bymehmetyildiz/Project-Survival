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

        player.ResetActiveWeapons();

        if (player.aimIndex == 0.0f)
        {
            player.StartCoroutine(player.BlendRig(1.0f, player.pistolAimRig));
            player.weapons[0].SetActive(true);
        }
        else if (player.aimIndex == 0.5f)
            player.weapons[1].SetActive(true);
        else if (player.aimIndex == 1.0f)
            player.weapons[2].SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.pistolAimRig.weight = 0.0f;
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
            for (int i = 0; i < player.weapons.Length; i++)
            {
                player.weapons[i].SetActive(false);
            }
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (player.aimIndex != 0.0f)
            {
                player.aimIndex = 0.0f;
                stateMachine.ChangeState(player.drawWeaponState);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (player.aimIndex != 0.5f)
            {
                player.aimIndex = 0.5f;
                stateMachine.ChangeState(player.drawWeaponState);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (player.aimIndex != 1.0f)
            {
                player.aimIndex = 1.0f;
                stateMachine.ChangeState(player.drawWeaponState);
            }
        }

    }
}

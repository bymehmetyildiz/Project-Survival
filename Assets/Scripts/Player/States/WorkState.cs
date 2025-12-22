using UnityEngine;

public class WorkState : PlayerState
{
    public WorkState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.NearestRecource().GetComponent<Tree>().state == TreeState.Standing)
            player.animator.SetInteger("ChopType", 0);
        else
            player.animator.SetInteger("ChopType", 1);

        player.woodAxe.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.woodAxe.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (player.NearestRecource() != null && !player.IsMoving())
        {
            // Rotate smoothly toward the closest resource
            Vector3 direction = (player.NearestRecource().transform.position - player.transform.position).normalized;
            direction.y = 0; // prevent tilting up/down

            Quaternion rot = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, player.turnSpeed * Time.deltaTime);

            // Move smoothly toward the closest resource
            if ((player.NearestRecource().transform.position - player.transform.position).magnitude < 1.5f)
            {
                player.cc.Move(-direction * Time.deltaTime * player.moveSpeed);
            }

           
        }


        player.ApplyGravity();

        if (triggerCalled)
        {
            if (!player.CanInteractWitResource())
                stateMachine.ChangeState(player.idleState);
            else if (player.IsMoving())
                stateMachine.ChangeState(player.moveState);
        }
    }
}

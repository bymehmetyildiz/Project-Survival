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

            player.transform.position = player.transform.position + direction * 1.2f;
        }


        player.ApplyGravity();

        if (!player.CanInteractWitResource() && triggerCalled)
            stateMachine.ChangeState(player.idleState);
        else if(player.IsMoving())
            stateMachine.ChangeState(player.moveState);
    }
}

using UnityEngine;

public class PickUpState : PlayerState
{
    public PickUpState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.animator.applyRootMotion = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.animator.applyRootMotion = false;
    }

    public override void Update()
    {
        base.Update();
        player.ApplyGravity();

        if (player.NearestCollectibleResource() != null && !player.IsMoving())
        {
            // Rotate smoothly toward the closest resource
            Vector3 direction = (player.NearestCollectibleResource().transform.position - player.transform.position).normalized;
            direction.y = 0; // prevent tilting up/down

            Quaternion rot = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, player.turnSpeed * Time.deltaTime);
        }

        if (triggerCalled)
            stateMachine.ChangeState(player.carryIdleState);
    }
}

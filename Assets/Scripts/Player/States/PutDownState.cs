using UnityEngine;

public class PutDownState : PlayerState
{
    public PutDownState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine(player.BlendCarryRig(0f));
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

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}

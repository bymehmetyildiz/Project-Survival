using UnityEngine;

public class PutDownState : PlayerState
{
    public PutDownState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.animator.applyRootMotion = true;
        player.StartCoroutine(player.BlendRig(0f, player.carryRig));
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

        if (triggerCalled)        
            stateMachine.ChangeState(player.idleState);
        

    }
}

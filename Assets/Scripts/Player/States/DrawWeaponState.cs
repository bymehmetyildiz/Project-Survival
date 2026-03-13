using UnityEngine;

public class DrawWeaponState : PlayerState
{
    public DrawWeaponState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)        
            stateMachine.ChangeState(player.aimState);
    }
}

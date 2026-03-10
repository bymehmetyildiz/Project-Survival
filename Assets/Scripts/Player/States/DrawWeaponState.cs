using UnityEngine;

public class DrawWeaponState : PlayerState
{
    public DrawWeaponState(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        for (int i = 0; i < player.weapons.Length; i++)
        {
            player.weapons[i].SetActive(false);
        }
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

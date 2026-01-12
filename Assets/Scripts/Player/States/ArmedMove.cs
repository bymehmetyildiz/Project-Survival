using UnityEngine;

public class ArmedMove : PlayerState
{
    public ArmedMove(StateMachine stateMachine, string animBoolName, CharacterController controller, Player player) : base(stateMachine, animBoolName, controller, player)
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangeState(player.moveState);
            player.weapon.SetActive(false);
            return;
        }

        player.cc.Move(player.Movement() * Time.deltaTime);

        if (player.IsMoving() == false)
            stateMachine.ChangeState(player.aimState);
        
    }
}

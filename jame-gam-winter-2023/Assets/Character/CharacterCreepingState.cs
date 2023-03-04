using UnityEngine;

public class CharacterCreepingState : CharacterBaseState
{
    // private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    // private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    // private const float AnimationDampTime = 0.1f;
    // private const float CrossFadeDuration = 0.1f;

    public CharacterCreepingState (CharacterStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // stateMachine.Velocity.y = Physics.gravity.y;

        // stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);

        // stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
    }

    public override void UpdateState()
    {
        if (!stateMachine.Movement.IsGrounded())
        {
            // TODO add fall state
            // stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();
        ApplyGravity();
        TryWallClimb();

        // stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        // stateMachine.InputReader.OnJumpPerformed -= SwitchToJumpState;
    }

    // private void SwitchToJumpState()
    // {
    //     stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    // }
}
using UnityEngine;

public abstract class CharacterBaseState : State
{
    protected readonly CharacterStateMachine stateMachine;

    protected CharacterBaseState(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void CalculateMoveDirection()
    {
        Vector3 cameraForward = new Vector3(stateMachine.MainCamera.forward.x, 0, stateMachine.MainCamera.forward.z);
        Vector3 cameraRight = new Vector3 (stateMachine.MainCamera.right.x, 0, stateMachine.MainCamera.right.z);

        Vector3 moveDirection = cameraForward.normalized * stateMachine.CharacterInput.MoveComposite.y + cameraRight.normalized * stateMachine.CharacterInput.MoveComposite.x;
        stateMachine.MoveDirection = moveDirection;
    }

    protected void ApplyGravity()
    {
        stateMachine.Movement.ApplyGravity();   
    }

    protected void Move()
    {
        stateMachine.Movement.Move(stateMachine.MoveDirection, stateMachine.CharacterInput.MoveComposite);
    }

    protected void TryWallClimb()
    {
        stateMachine.Movement.TryWallClimb();
    }
}
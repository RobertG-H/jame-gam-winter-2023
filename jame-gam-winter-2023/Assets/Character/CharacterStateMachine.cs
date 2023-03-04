using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public Vector3 MoveDirection = Vector3.zero;
    public float LookRotationDampFactor = 10f;
    public Transform MainCamera { get; private set; }
    public CharacterInput CharacterInput { get; private set; }
    // public Animator Animator { get; private set; }
    public CharacterMovement Movement { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main.transform;

        CharacterInput = GetComponent<CharacterInput> ();
        // Animator = GetComponent<Animator>();
        Movement = GetComponent<CharacterMovement>();

        SwitchState(new CharacterCreepingState(this));
    }
}
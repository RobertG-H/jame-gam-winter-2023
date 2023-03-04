using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public Vector3 Velocity;
    public float MovementSpeed = 5f;
    public float LookRotationDampFactor = 10f;
    public Transform MainCamera { get; private set; }
    public CharacterInput CharacterInput { get; private set; }
    // public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main.transform;

        CharacterInput = GetComponent<CharacterInput> ();
        // Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();

        SwitchState(new CharacterCreepingState(this));
    }
}
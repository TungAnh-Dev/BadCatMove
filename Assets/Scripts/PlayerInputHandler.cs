using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";
    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string fire = "Fire";

    InputAction moveAction;
    InputAction jumpAction;
    InputAction fireAction;

    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set;}
    public bool FireTriggered { get; private set;}

    public static PlayerInputHandler Instance {get; private set;}

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        fireAction = playerControls.FindActionMap(actionMapName).FindAction(fire);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        fireAction.performed += context => FireTriggered = true;
        fireAction.canceled += context => FireTriggered = false;
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        fireAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        fireAction.Disable();
    }
}
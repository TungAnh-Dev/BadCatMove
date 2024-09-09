
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    private CharacterController characterController;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public float jumpForce = 10f;
    public float maxSpeed = 20f;
    private PlayerInputHandler inputHandler;
    private Vector3 currentMovement;

    float gravity = -9.81f;
    float groundedGravity = -.05f;

    //jumping 
    [Header("Jumping")]
    public float initialJumpVelocity;
    float maxJumpHeight = 1f;
    float maxJumpTime = 0.5f;

    bool isJumping = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
        SetUpJumpVariables();
    }



    void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
        

        HandleGravity();
        HandleJump();
    }

    private void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }


    private void HandleGravity()
    {  
        bool isFalling = currentMovement.y <= 0.0f;
        float fallMultiplier = 2f;
        if(characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
        }
        // else if (isFalling)
        // {
        //     float previousYVeclocity = currentMovement.y;
        //     float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
        //     float nextYVelocity = Mathf.Max((previousYVeclocity + newYVelocity) * .5f, -20f);
        //     currentMovement.y = nextYVelocity;
        // }
        else
        {
            // float previousYVelocity = currentMovement.y;
            // float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            // float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            // currentMovement.y = nextYVelocity;

            currentMovement.y += gravity * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        // Get input and create a movement direction vector
        currentMovement.x = inputHandler.MoveInput.x;
        currentMovement.z = inputHandler.MoveInput.y;
        
        // Apply movement
        Vector3 movement = currentMovement * moveSpeed * Time.deltaTime;
        characterController.Move(movement);

    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Quaternion currentRotation = transform.rotation;

        if(inputHandler.MoveInput.x != 0 || inputHandler.MoveInput.y != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void HandleJump()
    {
        if(!isJumping && characterController.isGrounded && inputHandler.JumpTriggered)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
        }
    }
}

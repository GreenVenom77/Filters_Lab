using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using FishNet.Connection;
using FishNet.Object;

public class Player_Controller : NetworkBehaviour
{
    [Header("Variables")]
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float rotationFactorPerFrame = 13f;
    public CinemachineVirtualCamera Player_VCam;

    //Declares
    private Input_Actions playerInput;
    private CharacterController characterController;
    
    //Parameters
    private Vector2 currentMovementInput;
    private Vector3 movementDirection;
    private bool isMoving;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Player_VCam = FindObjectOfType<CinemachineVirtualCamera>();
            Player_VCam.Follow = transform;
            Player_VCam.LookAt = transform;
            GetComponentInChildren<Canvas>().enabled = true;
        }
        else
        {
            GetComponent<Player_Controller>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    void Awake()
    {
        playerInput = new Input_Actions();
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Get the forward direction of the camera without the y component
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Read input from the new input system
        currentMovementInput = playerInput.Player.Movement.ReadValue<Vector2>();

        // Calculate the movement direction in world space
        movementDirection = cameraForward * currentMovementInput.y + Camera.main.transform.right * currentMovementInput.x;

        // Apply movement
        characterController.Move(movementDirection * (Speed * Time.deltaTime));
        characterController.SimpleMove(Physics.gravity);
    }

    void HandleRotation()
    {
        Quaternion currentRotation = transform.rotation;

        // Read input from the new input system to change the boolean value
        isMoving = currentMovementInput != Vector2.zero;

        if (isMoving)
        {
            // Use the camera's forward direction to determine the rotation
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    
    private void OnDisable()
    {
        playerInput.Disable();
    }

}

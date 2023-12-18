using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FishNet.Component.Spawning;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Connection;
using FishNet.Object;

public class Player_Controller : NetworkBehaviour
{
    [Header("Variables")]
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float rotationFactorPerFrame = 13f;
    [SerializeField] private CinemachineVirtualCamera Player_VCam;

    //Declares
    private Input_Actions playerInput;
    private CharacterController characterController;
    private PlayerSpawner _playerSpawner;
    
    //Parameters
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMoving;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Player_VCam = GameObject.Find("Player Virtual Camera").GetComponent<CinemachineVirtualCamera>();
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

    public override void OnStopClient()
    {
        base.OnStopClient();
        _playerSpawner.players.RemoveAll(player => !player);
    }

    void Awake()
    {
        playerInput = new Input_Actions();
        characterController = GetComponent<CharacterController>();

        //Player Actions
        playerInput.Player.Movement.started += MovementInput;
        playerInput.Player.Movement.performed += MovementInput;
        playerInput.Player.Movement.canceled += MovementInput;
    }
    
    void Update()
    {
        HandleRotation();
        characterController.Move(currentMovement * (Speed * Time.deltaTime));
        characterController.SimpleMove(Physics.gravity);
    }
    
    void MovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement = CalculateMoveDirection(currentMovementInput);
        isMoving = currentMovementInput.sqrMagnitude > 0.01f;
    }

    void HandleRotation()
    {
        if (isMoving)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 targetDirection = Quaternion.FromToRotation(Vector3.forward, cameraForward) * currentMovement;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        return moveDirection.normalized;
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

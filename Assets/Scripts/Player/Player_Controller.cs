using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Connection;
using FishNet.Object;

public class Player_Controller : NetworkBehaviour
{
    [Header("Variables")]
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float rotationFactorPerFrame = 13f;
    
    //Declares
    private Input_Actions playerInput;
    private CharacterController characterController;
    private CinemachineVirtualCamera Player_VCam;
    
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
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMoving = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            
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

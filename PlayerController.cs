using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public PlayerStamina stamina;

    [Header("Movement Settings")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f; // Lowered for testing
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Crouch Settings")]
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    [Header("Look Settings")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Jump Forgiveness")]
    public float coyoteTime = 0.2f;
    private float coyoteTimer;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove = true;

    private float originalWalkSpeed;
    private float originalRunSpeed;
    private bool isCrouching = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
    }

    void Update()
    {
        Debug.Log("Grounded: " + characterController.isGrounded); // Optional debug

        HandleMovement();
        HandleLook();
        HandleCrouchToggle();

        // Update coyote time for jump forgiveness
        if (characterController.isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;
    }

    void HandleMovement()
    {
        if (!canMove) return;

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        float currentSpeed = walkSpeed;

        bool isTryingToRun = Input.GetKey(KeyCode.LeftShift) && inputZ > 0;

        if (isTryingToRun && stamina != null && stamina.HasStamina())
        {
            currentSpeed = runSpeed;
        }

        Vector3 move = transform.TransformDirection(new Vector3(inputX, 0, inputZ)) * currentSpeed;

        // Stick to ground or apply gravity
        if (characterController.isGrounded)
        {
            moveDirection.y = -1f;

            if (Input.GetButtonDown("Jump") && coyoteTimer > 0f)
            {
                moveDirection.y = jumpPower;
                coyoteTimer = 0f;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        Vector3 finalMove = move;
        finalMove.y = moveDirection.y;

        characterController.Move(finalMove * Time.deltaTime);
        moveDirection.y = finalMove.y;
    }

    void HandleLook()
    {
        if (!canMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleCrouchToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                characterController.height = crouchHeight;
                characterController.center = new Vector3(0, crouchHeight / 2f, 0);
                walkSpeed = crouchSpeed;
                runSpeed = crouchSpeed;
            }
            else
            {
                characterController.height = defaultHeight;
                characterController.center = new Vector3(0, defaultHeight / 2f, 0);
                walkSpeed = originalWalkSpeed;
                runSpeed = originalRunSpeed;
            }
        }
    }
}
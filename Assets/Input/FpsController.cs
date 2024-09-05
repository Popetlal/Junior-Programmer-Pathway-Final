using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FpsController : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.8f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 1.0f;
    [SerializeField] private float upDownRange = 80.0f;

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private Vector3 currentMovement;
    private float verticalRotation;
    private Animation ADS_M4;
    private InputAction move;
    private bool isReloading;



    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private GameObject gun;
    [SerializeField] private AudioSource gunFire;
    [SerializeField] private int ammo;
    [SerializeField] private TextMeshProUGUI ammoText;

    // Start is called before the first frame update

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;
        ADS_M4 = GetComponent<Animation>();
        ammo = 60;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCrouching();
        // HandleJumping();
        HandleAds();
        HandleFire();
        HandleReload();

    }

    void HandleMovement()
    {
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);

        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;

        HandleJumping();

        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        
    }



    void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (inputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    void HandleCrouching()
    {
        if (inputHandler.CrouchValue)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        } else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void HandleAds()
    {
        if (inputHandler.ADSValue)
        {
            gun.transform.localPosition = new Vector3(0.303f, 1.494f, -0.004f);
        }
        else
        {
            gun.transform.localPosition = new Vector3(-0.21f, 1.73f, 0.67f);
        }

    }

    void HandleReload()
    {
        if (inputHandler.ReloadValue)
        {
            isReloading = true;
            Invoke("ReloadFunction", 1);
        }
    }

    void HandleFire()
    {
        if (inputHandler.FireValue && isReloading == false)
        {
            if (ammo <= 0)
            {
                ammo = 0;
                ammoText.text = $"0/∞";
            } else
            {
                gunFire.Play();
                ammo -= 1;
                ammoText.text = $"{ammo}/∞";
            }
            
        }
    }

    void ReloadFunction()
    {
        Debug.Log("Reload");
        ammo = 60;
        ammoText.text = $"{ammo}/∞";
        isReloading = false;
    }

}

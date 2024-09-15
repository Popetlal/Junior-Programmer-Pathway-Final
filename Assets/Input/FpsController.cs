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
    private EnemyMovement enemyMovement;
    private Vector3 randomSpawnPos;
<<<<<<< Updated upstream
=======
    private int ShotsFired;
    private int TargetsHit;
>>>>>>> Stashed changes

    private int targetsHit;
    private int shotsFired;

    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private GameObject gun;
    [SerializeField] private AudioSource gunFire;
    [SerializeField] private int ammo;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private LayerMask layerMask;
<<<<<<< Updated upstream
=======

    [SerializeField] private TextMeshProUGUI shotsFiredText;
    [SerializeField] private TextMeshProUGUI targetsHitText;

    [SerializeField] private GameObject BananaMan;
    [SerializeField] private GameObject BananaMan1;
    [SerializeField] private GameObject BananaMan2;
>>>>>>> Stashed changes

    [SerializeField] private GameObject BananaMan;
    [SerializeField] private GameObject BananaMan1;
    [SerializeField] private GameObject BananaMan2;

    [SerializeField] private TextMeshProUGUI targetHitText;
    [SerializeField] private TextMeshProUGUI shotsFiredText;
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
        enemyMovement = GameObject.Find("BananaMan").GetComponent<EnemyMovement>();
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

        randomSpawnPos = new Vector3(Random.Range(-16, +10), 0, Random.Range(+4, +26));

<<<<<<< Updated upstream
        targetHitText.text = $"Targets Hit: {targetsHit}";
        shotsFiredText.text = $"Shots Fired: {shotsFired}";
=======
        targetsHitText.text = $"Targets Hit: {TargetsHit}";
        shotsFiredText.text = $"Shots Fired: {ShotsFired}";
>>>>>>> Stashed changes
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
        }
        else
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
            }
            else
            {
                gunFire.Play();
                ammo -= 1;
<<<<<<< Updated upstream
                shotsFired++;
=======
                ShotsFired++;
>>>>>>> Stashed changes
                ammoText.text = $"{ammo}/∞";

                Ray bullet = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                Debug.DrawRay(bullet.origin, bullet.direction * 9999);
                RaycastHit hitInfo;
                if (Physics.Raycast(bullet, out hitInfo, 9999, layerMask))
                {
                    GameObject parent = hitInfo.collider.gameObject.transform.parent.gameObject;
                    string name = parent.name;
                    Debug.Log($"Name: {name}");
                    string enemyName = "";
                    GameObject enemy = GameObject.Find(name);
<<<<<<< Updated upstream
                    targetsHit++;
                    
=======
                    TargetsHit++;
>>>>>>> Stashed changes

                    switch (name)
                    {
                        case "BananaMan":
                            enemyName = "BananaMan";
                            GameObject newBananaMan = Instantiate(BananaMan, randomSpawnPos, Quaternion.Euler(0, 90, 0));
                            if (newBananaMan.GetComponent<EnemyMovement>() == null)
                            {
                                newBananaMan.AddComponent<EnemyMovement>();
                            }
                            newBananaMan.name = "BananaMan";
                            break;

                        case "BananaMan1":
                            enemyName = "BananaMan1";
                            GameObject newBananaMan1 = Instantiate(BananaMan1, randomSpawnPos, Quaternion.Euler(0, 90, 0));
                            if (newBananaMan1.GetComponent<EnemyMovement>() == null)
                            {
                                newBananaMan1.AddComponent<EnemyMovement>();
                            }
                            newBananaMan1.name = "BananaMan1";
                            break;

                        case "BananaMan2":
                            enemyName = "BananaMan2";
                            GameObject newBananaMan2 = Instantiate(BananaMan2, randomSpawnPos, Quaternion.Euler(0, 90, 0));
                            if (newBananaMan2.GetComponent<EnemyMovement>() == null)
                            {
                                newBananaMan2.AddComponent<EnemyMovement>();
                            }
                            newBananaMan2.name = "BananaMan2";

                            break;
                    }

                    Destroy(parent);


<<<<<<< Updated upstream
                }
            }

=======
                } else
                {
                    Debug.Log("miss");
                }
            }            
>>>>>>> Stashed changes
        }
    }

    void ReloadFunction()
    {
        ammo = 60;
        ammoText.text = $"{ammo}/∞";
        isReloading = false;
    }

<<<<<<< Updated upstream


}
=======
    

}
>>>>>>> Stashed changes

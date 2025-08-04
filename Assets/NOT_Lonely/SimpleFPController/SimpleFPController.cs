using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class SimpleFPController : MonoBehaviour
{
    [Header("MOUSE LOOK")]
    public Vector2 mouseSensitivity = new Vector2(20, 20);
    public Vector2 verticalLookLimit = new Vector2(-85, 85);
    public float smooth = 0.05f;

    private float xRot;
    private Camera cam;

    [Header("MOVEMENT")]
    public bool physicsController = false;
    public float walkSpeed = 1;
    public float runSpeed = 3;
    //public float jumpForce = 2;
    private float speed = 1;

#if ENABLE_INPUT_SYSTEM
    [Header("CONTROLS")]
    public Key forward = Key.W;
    public Key backward = Key.S;
    public Key strafeLeft = Key.A;
    public Key strafeRight = Key.D;
    public Key run = Key.LeftShift;
    //public Key jump = Key.Space;
#else
[Header("CONTROLS")]
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode strafeLeft = KeyCode.A;
    public KeyCode strafeRight = KeyCode.D;
    public KeyCode run = KeyCode.LeftShift;
    //public KeyCode jump = KeyCode.Space;
#endif

    [Header("SIGHT")]
    public bool sight = false;
    public GameObject sightPrefab;

    [Header("AUDIO")]
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip walkSound;
    public AudioClip runSound;

    //private Rigidbody rb;

    public bool hideCursor = false;

    private bool forwardMove = false;
    private bool backwardMove = false;
    private bool leftMove = false;
    private bool rightMove = false;

    private CharacterController controller;
    private Animator animator;

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); 

        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        //rb = GetComponent<Rigidbody>();

        // Initialize audio sources if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        if (audioSource2 == null)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }
        
        // Setup audio sources
        if (walkSound != null && audioSource != null)
        {
            audioSource.clip = walkSound;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
        
        if (runSound != null && audioSource2 != null)
        {
            audioSource2.clip = runSound;
            audioSource2.loop = true;
            audioSource2.playOnAwake = false;
        }

        if (hideCursor && !PauseManager.isGamePaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (sight)
        {
            GameObject sightObj = Instantiate(sightPrefab);
            sightObj.transform.SetParent(transform.parent);
        }
    }

    void Update()
    {
        if (PauseManager.isGamePaused)
            return;
        CameraLook();

        PlayerMove();

        bool isAnyMovement = forwardMove || backwardMove || leftMove || rightMove;

        if (animator != null)
        {
            animator.SetBool("isMoving", isAnyMovement);
            animator.SetBool("isRunning", isAnyMovement && speed == runSpeed);
        }

        // Handle footstep audio - simple approach
        HandleSimpleAudio(isAnyMovement, speed == runSpeed);
    }

    void HandleSimpleAudio(bool isMoving, bool isRunning)
    {
        if (audioSource == null || audioSource2 == null) return;

        // Check if game is paused or in dialogue - stop all movement sounds
        bool inDialogue = IsInDialogue();
        bool movementDisabled = !this.enabled; // Check if this script is disabled
        
        if (PauseManager.isGamePaused || inDialogue || movementDisabled)
        {
            // Debug logging to see if this condition is being triggered
            if (inDialogue)
            {
                Debug.Log("In dialogue - stopping footstep sounds");
            }
            if (movementDisabled)
            {
                Debug.Log("Movement disabled - stopping footstep sounds");
            }
            
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (audioSource2.isPlaying)
            {
                audioSource2.Stop();
            }
            return;
        }

        if (isMoving)
        {
            if (isRunning)
            {
                // Running: play run sound, stop walk sound
                if (runSound != null && !audioSource2.isPlaying)
                {
                    audioSource2.Play();
                }
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
            else
            {
                // Walking: play walk sound, stop run sound
                if (walkSound != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                if (audioSource2.isPlaying)
                {
                    audioSource2.Stop();
                }
            }
        }
        else
        {
            // Not moving: stop both sounds
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (audioSource2.isPlaying)
            {
                audioSource2.Stop();
            }
        }
    }

    // Check if player is currently in dialogue
    bool IsInDialogue()
    {
        // Check if DialogueManager exists and dialogue is active
        if (DialogueManager.Instance != null)
        {
            bool dialogueActive = DialogueManager.Instance.IsDialogueActive;
            if (dialogueActive)
            {
                Debug.Log("Dialogue is active - should stop footsteps");
            }
            return dialogueActive;
        }
        return false;
    }

    float refVelX;
    float refVelY;
    float xRotSmooth;
    float yRotSmooth;

    void CameraLook()
    {
#if ENABLE_INPUT_SYSTEM
        float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * mouseSensitivity.x;
        float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * mouseSensitivity.y;
#else
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity.x * 10;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity.y * 10;
#endif

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, verticalLookLimit.x, verticalLookLimit.y);

        xRotSmooth = Mathf.SmoothDamp(xRotSmooth, xRot, ref refVelX, smooth);
        yRotSmooth = Mathf.SmoothDamp(yRotSmooth, mouseX, ref refVelY, smooth);

        cam.transform.localEulerAngles = new Vector3(xRotSmooth, 0, 0);
        transform.Rotate(Vector3.up * yRotSmooth);
    }

    void PlayerMove()
    {
#if ENABLE_INPUT_SYSTEM

        if (Keyboard.current[run].isPressed)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (Keyboard.current[forward].isPressed)
        {
            forwardMove = true;
        }
        else
        {
            forwardMove = false;
        }

        if (Keyboard.current[backward].isPressed)
        {
            backwardMove = true;
        }
        else
        {
            backwardMove = false;
        }

        if (Keyboard.current[strafeLeft].isPressed)
        {
            leftMove = true;
        }
        else
        {
            leftMove = false;
        }
        if (Keyboard.current[strafeRight].isPressed)
        {
            rightMove = true;
        }
        else
        {
            rightMove = false;
        }
#else
        if (Input.GetKey(run))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (Input.GetKey(forward))
        {
            forwardMove = true;
        }
        else
        {
            forwardMove = false;
        }

        if (Input.GetKey(backward))
        {
            backwardMove = true;
        }
        else
        {
            backwardMove = false;
        }

        if (Input.GetKey(strafeLeft))
        {
            leftMove = true;
        }
        else
        {
            leftMove = false;
        }
        if (Input.GetKey(strafeRight))
        {
            rightMove = true;
        }
        else
        {
            rightMove = false;
        }
#endif
    }

    private void FixedUpdate()
    {
        if (forwardMove)
        {
            controller.Move(controller.transform.forward * speed * 0.01f);
        }

        if (backwardMove)
        {
            controller.Move(controller.transform.forward * -speed * 0.01f);
        }

        if (leftMove)
        {
            controller.Move(controller.transform.right * -speed * 0.01f);
        }
        if (rightMove)
        {
            controller.Move(controller.transform.right * speed * 0.01f);
        }

        if (controller.isGrounded) return;

        if(Physics.SphereCast(transform.position, controller.radius, -transform.up, out RaycastHit hitInfo, 10, -1, QueryTriggerInteraction.Ignore))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y + controller.height / 2 + controller.skinWidth, transform.position.z);
        }
    }
}

using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("References")]
    private CharacterController controller;
    [FoldoutGroup("References")]
    public InputSystem_Actions inputs;
    [FoldoutGroup("Movement Settings")]
    public float moveSpeed = 10f;
    [FoldoutGroup("Movement Settings")]
    public float rotationSpeed = 200f;
    
    [SerializeField] private Vector2 moveInput;
    [FoldoutGroup("Jump")]
    public float verticalVelocity = 0f;
    [FoldoutGroup("Jump")]
    public float JumpForce = 5f;

    public float pushForce = 2f;
    [FoldoutGroup("Dash")]
    public bool IsDashing = false;
    [FoldoutGroup("Dash")]
    public float dashForce = 20f;
    [FoldoutGroup("Dash")]
    public float dashDuration = 0.5f;
    [FoldoutGroup("Dash")]
    private float dashTimer = 0f;
    private bool isSprinting = false;
    private float baseMoveSpeed;

    [FoldoutGroup("Interact")]
    public Action OnInteractEvent;


    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();
        baseMoveSpeed = moveSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputs.Player.Jump.performed += Jump_performed;
       
        inputs.Player.Sprint.performed += OnSprint;
        inputs.Player.Sprint.canceled += OnSprintCanceled;

        inputs.Player.Interact.performed += OnInteract;
    }

    

    void Start()
    {

    }


    void Update()
    {
        Movement();       
    }




    public void Movement()
    {
      
        float currentSpeed = isSprinting ? baseMoveSpeed * 3 : baseMoveSpeed;

        Vector3 moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x) * currentSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        moveDir.y = verticalVelocity;

        if (IsDashing)
        {
            // dash in the forward direction without changing rotation
            moveDir = transform.forward * dashForce;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
                IsDashing = false;
        }

        controller.Move(moveDir * Time.deltaTime);

    }
    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (!controller.isGrounded) return;
        verticalVelocity = JumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 pushDir = (hit.transform.position - transform.position).normalized;
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(pushDir * pushForce, ForceMode.Impulse);
    }
  
    private void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Punto de inicio del rayo (posición del objeto)
        Vector3 start = transform.position;

        // Dirección del rayo (hacia adelante desde el objeto)
        Vector3 direction = transform.forward.normalized;

        // Dibujar el rayo desde la posición del objeto hacia adelante
        Gizmos.DrawRay(start, direction * 5f); // El 5f es la longitud del rayo
    }
    private void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInteractEvent?.Invoke();
        }
    }
}

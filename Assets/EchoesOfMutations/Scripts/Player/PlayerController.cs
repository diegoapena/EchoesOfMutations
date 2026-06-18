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
    [FoldoutGroup("Shoot Settings")]
    public Transform WeaponShootAnchor;
    [FoldoutGroup("Shoot Settings")]
    public LineRenderer RayPrefab;
    [FoldoutGroup("Movement Settings")]
    public float moveSpeed = 10f;
    [FoldoutGroup("Movement Settings")]
    [SerializeField] private Vector2 moveInput;
    [FoldoutGroup("Jump")]
    public float verticalVelocity = 0f;
    [FoldoutGroup("Jump")]
    public float JumpForce = 5f;

    //public float pushForce = 2f;
    
    private bool isSprinting = false;
    private float baseMoveSpeed;

    [FoldoutGroup("Interact")]
    public Action OnInteractEvent;
    public Action<float> OnScroolChanged;
    public Action OnEquipEvent;
   




    public LayerMask enemyMask;
    public Camera characterCamera;
    public Transform holdpoint;
    private Rigidbody grabbedObject;

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

        
        inputs.Player.Grab.performed += GrabObject;
        inputs.Player.Grab.canceled += ReleaseObject;

        inputs.Player.Interact.performed += OnInteract;

        inputs.Player.Next.performed += OnScroll;

    }

    

    private void OnDisable()
    {
        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputs.Player.Jump.performed -= Jump_performed;

        inputs.Player.Sprint.performed -= OnSprint;
        inputs.Player.Sprint.canceled -= OnSprintCanceled;

        
        inputs.Player.Grab.performed -= GrabObject;
        inputs.Player.Grab.canceled -= ReleaseObject;

        inputs.Player.Interact.performed -= OnInteract;
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
      
        float currentSpeed = isSprinting ? baseMoveSpeed * 2 : baseMoveSpeed;

        Vector3 moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x) * currentSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        moveDir.y = verticalVelocity;   

        controller.Move(moveDir * Time.deltaTime);

        if (GameManager.Instance.animationManager != null)
        {
            GameManager.Instance.animationManager.SetFloat("VelX", moveInput.x);
            GameManager.Instance.animationManager.SetFloat("VelY", moveInput.y);
        }

    }
    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (!controller.isGrounded) return;
        verticalVelocity = JumpForce;
    }

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 pushDir = (hit.transform.position - transform.position).normalized;
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(pushDir * pushForce, ForceMode.Impulse);
    } */
  
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
        Gizmos.DrawRay(start, direction * 2f); // El 5f es la longitud del rayo
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInteractEvent?.Invoke();
        }
    }
    private void OnScroll(InputAction.CallbackContext context)
    {
        /*
        float value = context.ReadValue<float>();
        float direcction = value > 0f ? -1 : 1f;
        OnScroolChanged?.Invoke(direcction);
        */
        Debug.Log("Change");
    }
    private void GrabObject(InputAction.CallbackContext ctx)
    {

        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            if (hit.collider.CompareTag("Box"))
            {
                grabbedObject = hit.collider.GetComponent<Rigidbody>();

                if (grabbedObject != null)
                {
                    grabbedObject.useGravity = false;
                    grabbedObject.transform.SetParent(holdpoint); 
                    grabbedObject.transform.localPosition = Vector3.zero; 
                    grabbedObject.transform.localRotation = Quaternion.identity; 
                }
            }
        }
    }
    private void ReleaseObject(InputAction.CallbackContext ctx)
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.transform.SetParent(null); 
            grabbedObject = null;
        }
    }

    /*private void OnAttack(InputAction.CallbackContext context)
    {
        if(Physics.SphereCast(WeaponShootAnchor.position , 3f, WeaponShootAnchor.transform.forward , out RaycastHit hit ,100 , enemyMask))
        {
            Debug.Log("Hit");
            LineRenderer ray = Instantiate(RayPrefab , transform.position , Quaternion.identity);
            ray.gameObject.transform.position = WeaponShootAnchor.position;
            ray.positionCount = 2;
            ray.SetPosition(0, WeaponShootAnchor.position);
            ray.SetPosition(1,hit.point);

            Quaternion rot = Quaternion.LookRotation(hit.normal);

            Destroy(ray, 2f);
        }
        else
        {
            Debug.Log("Shot miss");
        }
    }
    */
}

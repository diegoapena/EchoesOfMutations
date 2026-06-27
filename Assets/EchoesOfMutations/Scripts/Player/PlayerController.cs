using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("References")]
    private CharacterController controller;
    [FoldoutGroup("References")]
    public InputSystem_Actions inputs;

    [FoldoutGroup("Movement Settings")]
    public float moveSpeed = 10f;
    [FoldoutGroup("Movement Settings")]
    [SerializeField] private Vector2 moveInput;
    [FoldoutGroup("Jump")]
    public float verticalVelocity = 0f;
    [FoldoutGroup("Jump")]
    public float JumpForce = 5f;

    [SerializeField] private float distance = 2f;
    private bool isSprinting = false;
    private float baseMoveSpeed;

    [FoldoutGroup("Interact")]
    public static  Action OnInteractEvent;
    public static event Action<int> OnSlotSelected;
    public static event Action<float> OnSlotScroll;
    public static event Action OnInventory;
    public static event Action OnRemoveItem;
    public static event Action<CraftingStation> OnCraftingOpen;
    public static event Action OnTurnFlashlight;

    [SerializeField] private Transform gunMuzzle;   
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask Interactable;
    [SerializeField] private LineRenderer RayPrefab;
    


    public CinemachineCamera characterCamera;
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

        inputs.Player.Attack.performed += OnAttack;

        inputs.Player.FlashLight.performed += TurnObj;

        inputs.Player.Grab.performed += GrabObject;

        inputs.Player.Grab.canceled += ReleaseObject;

        inputs.Player.Interact.performed += OnInteract;

        inputs.Player.NextOrPrev.performed += OnScroll;

        inputs.Player.Remove.performed += SpawnObj;

        inputs.Player.Slot1.performed += ctx => OnSlotSelected?.Invoke(0);
        inputs.Player.Slot2.performed += ctx => OnSlotSelected?.Invoke(1);
        inputs.Player.Slot3.performed += ctx => OnSlotSelected?.Invoke(2);
        inputs.Player.Slot4.performed += ctx => OnSlotSelected?.Invoke(3);
        inputs.Player.Slot5.performed += ctx => OnSlotSelected?.Invoke(4);
        inputs.Player.Slot6.performed += ctx => OnSlotSelected?.Invoke(5);
        inputs.Player.Slot7.performed += ctx => OnSlotSelected?.Invoke(6);
        inputs.Player.Slot8.performed += ctx => OnSlotSelected?.Invoke(7);
        inputs.Player.Slot9.performed += ctx => OnSlotSelected?.Invoke(8);
    }

   

    private void OnDisable()
    {      
        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputs.Player.Jump.performed -= Jump_performed;

        inputs.Player.Sprint.performed -= OnSprint;
        inputs.Player.Sprint.canceled -= OnSprintCanceled;

        inputs.Player.Attack.performed -= OnAttack;

        inputs.Player.FlashLight.performed -= TurnObj;

        inputs.Player.Grab.performed -= GrabObject;

        inputs.Player.Grab.canceled -= ReleaseObject;

        inputs.Player.Interact.performed -= OnInteract;
       
        inputs.Player.NextOrPrev.performed -= OnScroll;

        inputs.Player.Remove.performed -= SpawnObj;

        inputs.Player.Slot1.performed -= ctx => OnSlotSelected?.Invoke(0);
        inputs.Player.Slot2.performed -= ctx => OnSlotSelected?.Invoke(1);
        inputs.Player.Slot3.performed -= ctx => OnSlotSelected?.Invoke(2);
        inputs.Player.Slot4.performed -= ctx => OnSlotSelected?.Invoke(3);
        inputs.Player.Slot5.performed -= ctx => OnSlotSelected?.Invoke(4);
        inputs.Player.Slot6.performed -= ctx => OnSlotSelected?.Invoke(5);
        inputs.Player.Slot7.performed -= ctx => OnSlotSelected?.Invoke(6);
        inputs.Player.Slot8.performed -= ctx => OnSlotSelected?.Invoke(7);
        inputs.Player.Slot9.performed -= ctx => OnSlotSelected?.Invoke(8);
        inputs.Disable();

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
  
    private void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
    private void TurnObj(InputAction.CallbackContext context)
    {        
        OnTurnFlashlight?.Invoke();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);


        if (!Physics.Raycast(ray, out RaycastHit itemhit, distance, Interactable ))
            return;

        //if(!Physics.Raycast(ray , out RaycastHit craftitem , distance , Barricades ))
        
        CraftingStation station = itemhit.collider.GetComponent<CraftingStation>();
        if (station != null) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnCraftingOpen?.Invoke(station);
            return;
        }
        else
        {
            Cursor.visible = false;
        }

            IInteractable interactable = itemhit.collider.GetComponent<IInteractable>();
        Debug.Log(itemhit.collider.name);
        if (interactable != null)
        {
            OnInteractEvent?.Invoke();
            interactable.Interact();
        }  
    }
    private void OnScroll(InputAction.CallbackContext context) => OnSlotScroll?.Invoke(context.ReadValue<Vector2>().y);
    
    private void GrabObject(InputAction.CallbackContext ctx)
    {     
        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            
            if (hit.collider.CompareTag("MetalBox") || hit.collider.CompareTag("WoodBox"))
            {
                if (hit.collider.gameObject == null) return;    
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
    private void SpawnObj(InputAction.CallbackContext context)
    {
        OnRemoveItem?.Invoke();
    }   
    private void OnAttack(InputAction.CallbackContext context)
    {
      

        if(Physics.SphereCast(gunMuzzle.position, 5f, gunMuzzle.transform.forward, out RaycastHit hit, 100f, enemyMask))
        {
            if (hit.collider.gameObject == null) return;
            Debug.Log("Enemy hit" + hit.collider.name);
            LineRenderer ray = Instantiate(RayPrefab, transform.position, Quaternion.identity);
            ray.gameObject.transform.position = gunMuzzle.position;
            ray.positionCount = 2;
            ray.SetPosition(0, gunMuzzle.position);
            ray.SetPosition(1, hit.point);
            Quaternion rot = Quaternion.LookRotation(hit.normal);

            GameObject obj = hit.collider.gameObject;
            obj.GetComponent<BaseEnemy>().RecieveDamage(3);
            Destroy(ray, 2f);
        }
        else
        {
            Debug.Log("Shot miss");
        }
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

        Gizmos.color = Color.green;
        Gizmos.DrawLine(characterCamera.transform.position, characterCamera.transform.position + characterCamera.transform.forward * 2f);
    }

}

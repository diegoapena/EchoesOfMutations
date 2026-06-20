using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : BaseInteractableObj, IInteractable
{
    [SerializeField] private bool isInInventory = false;
    [SerializeField] private bool isOn = false; 
    
    private Light flashlightLight;

    

    private void Awake()
    {
       
        flashlightLight = GetComponent<Light>();
        flashlightLight.enabled = false; 
      
    }

    private void OnEnable()
    { 
        //PlayerController.OnInteractEvent += TryPickUp;
        GameManager.Instance.playerManager.playerController.inputs.Player.FlashLight.performed += OnFlashLightAction;
    }

  
    void Update()
    {
       
    
    }

    public void Interact(Transform interactor)
    {
        if (isInInventory)
        {
            ToggleFlashlight(); 
        }
    }

    /*
    public void TryPickUp()
    {
        if (itemData == null) return;

        if (Vector3.Distance(transform.position, GameManager.Instance.playerManager.transform.position) < 2.5f)
        {
            GameManager.Instance.playerManager.playerMechanics.GrabItem(gameObject);
            isInInventory = true;
                           
        }
    }
    */

    private void OnFlashLightAction(InputAction.CallbackContext context)
    {
        if (isInInventory)
        {
            ToggleFlashlight(); 
        }
    }

    private void ToggleFlashlight()
    {
        if (flashlightLight != null)
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn; 
        }
    }
}
    
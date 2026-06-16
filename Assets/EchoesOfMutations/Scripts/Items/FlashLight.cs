using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour, IInteractable
{
    private bool isInInventory = false; 
    private bool isOn = false; 
    
    private Light flashlightLight; 
    

    private void Awake()
    {
       
        flashlightLight = GetComponent<Light>();
        flashlightLight.enabled = false; 
      
    }

    private void OnEnable()
    {
       
        
            GameManager.Instance.playerManager.playerController.OnInteractEvent += DetectPosition;
            GameManager.Instance.playerManager.playerController.inputs.Player.FlashLight.performed += OnFlashLightAction;
        
       
    }

    private void OnDisable()
    {
       
        
            GameManager.Instance.playerManager.playerController.OnInteractEvent -= DetectPosition;
            GameManager.Instance.playerManager.playerController.inputs.Player.FlashLight.performed -= OnFlashLightAction;
        
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

    public void DetectPosition()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.playerManager.transform.position) < 2.5f)
        {
            GameManager.Instance.playerManager.playerMechanics.PickUp(gameObject);
            isInInventory = true; 
           
        }
    }

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
    
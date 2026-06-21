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

    }

  
    void Update()
    {
       
    
    }

    
    
   

    public void ToggleFlashlight()
    {
        if (flashlightLight != null)
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn; 
        }
    }  
}
    
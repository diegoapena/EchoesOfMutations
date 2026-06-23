using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : BaseInteractableObj, IInteractable
{  
    [SerializeField] private bool isOn = false; 
    
    private Light flashlightLight;

    

    private void Awake()
    {
       
        flashlightLight = GetComponent<Light>();
        flashlightLight.enabled = false; 
      
    }

    private void OnEnable()
    {
        PlayerController.OnTurnFlashlight += TurnOnOrOff;
    }
    private void OnDisable()
    {
        PlayerController.OnTurnFlashlight -= TurnOnOrOff;
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
    private void TurnOnOrOff()
    {
        if (isInInventory)
        {
            ToggleFlashlight();
        }

    }

}
    
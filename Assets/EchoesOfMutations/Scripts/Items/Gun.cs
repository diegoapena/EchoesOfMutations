using System;
using UnityEngine;

public class Gun : BaseInteractableObj
{
    public bool IsInInventory = false;

    private void Awake()
    {
        //PlayerController.OnInteractEvent += TryPickUp;
    }

    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    /*
    private void TryPickUp()
    {
        if (itemData == null) return;
        GameManager.Instance.playerManager.playerMechanics.GrabItem(gameObject);
        IsInInventory = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryPickUp();
        }
    }
    */
}

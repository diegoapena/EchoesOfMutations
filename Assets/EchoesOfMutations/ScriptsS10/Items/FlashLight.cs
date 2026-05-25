using UnityEngine;

public class FlashLight : MonoBehaviour , IInteractable
{
    //This scrip is still in test

    private void OnEnable()
    {
        GameManager.Instance.playerManager.playerController.OnInteractEvent += DetectPosition;
    }
    private void OnDisable()
    {
        GameManager.Instance.playerManager.playerController.OnInteractEvent -= DetectPosition;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    public void Interact(Transform interactor)
    {       
        interactor = GameManager.Instance.playerManager.playerMechanics.ItemContainer.transform;
        transform.position = interactor.position;      
    }
    
    

    public void DetectPosition()
    {
        if(Vector3.Distance(transform.position, GameManager.Instance.playerManager.transform.position) < 1.5f)
        {
            GameManager.Instance.playerManager.playerMechanics.PickUp(gameObject);
        }
    }
}

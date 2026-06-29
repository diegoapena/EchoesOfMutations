using UnityEngine;

public class BaseMaterialPickUp : MonoBehaviour, IInteractable
{
    public BaseMaterialData materialData;
    public int amount = 1;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float duration = 5f;   
    private float elapsedTime = 0f;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Interact()
    {
       if(materialData == null)
       {
            Debug.LogWarning("MaterialPickup : No materials assigned on : " + gameObject.name);
            return;
       }

        InventoryManager.Instance.AddMaterial(materialData, amount);
        Debug.Log("Picked up :" + materialData.MaterialName + " x " + amount);

        Destroy(gameObject);
    }
    public void Transition()
    {
 
        if (elapsedTime < duration)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
        
    }
}

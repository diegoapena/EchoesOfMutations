using UnityEngine;


public class BaseCraftable : MonoBehaviour , IDamageable , IInteractable
{
    [SerializeField] private BaseItemsData itemData;
    [SerializeField] protected bool isInInventory = false;
    [SerializeField] private Vector3 originalScale;   
    public float durability;
    private Vector3 equipPosition;    
    private Vector3 equipRotation;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    public void RecieveDamage(float damage)
    {     
        durability -= damage;
    }
    
    public void Interact()
    {
        if (itemData.ItemType == ItemsTypes.Interactable || itemData.ItemType == ItemsTypes.Craftable)
        {
            Debug.Log("Item addeded to slot");
            InventoryManager.Instance.AddItem(this);
        }
    }   
    public void OnEquip(Transform equipPoint)
    {
        equipPoint = GameManager.Instance.playerManager.playerMechanics.ItemContainer;
        isInInventory = true;
        transform.SetParent(equipPoint);
        transform.localPosition = equipPosition;
        transform.localRotation = Quaternion.Euler(equipRotation);
        transform.localScale = originalScale;
        gameObject.SetActive(true);
    }
    
    public void OnUnEquipped()
    {
        gameObject.SetActive(false);
        isInInventory = false;
    }   
    
    public void OnPlaceItem(Vector3 position)
    {
        transform.SetParent(null);
        transform.position = position;
        transform.localScale = originalScale;
        gameObject.SetActive(true);
    }   

    public BaseItemsData ItemData => itemData;
}

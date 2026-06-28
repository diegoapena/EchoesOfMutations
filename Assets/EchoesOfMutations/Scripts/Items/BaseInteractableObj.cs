using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BaseInteractableObj : MonoBehaviour , IInteractable
{
    [SerializeField] protected BaseItemsData itemData;
    [SerializeField] protected bool isInInventory = false;
    [SerializeField] private Vector3 originalScale;
    [SerializeField] protected Rigidbody rb;
    private Vector3 equipPosition;    
    private Vector3 equipRotation;

    private void Awake()
    {       
    }
    void Start()
    {
        
    }

    void Update()
    {
        
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
        rb.isKinematic = true;
        rb.useGravity = false;
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
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.SetParent(null);
        transform.position = position;
        transform.position = position;
        transform.localScale = originalScale;
        gameObject.SetActive(true);
    }
    
    public BaseItemsData ItemData => itemData;   
}

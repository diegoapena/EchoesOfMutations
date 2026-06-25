using UnityEngine;

public class BaseCraftable : MonoBehaviour , IDamageable //, IInteractable
{
    [SerializeField] private BaseItemsData itemData;
    [SerializeField] private bool isInInventory = false;
    public float durability;
    private Vector3 equipPosition;
    [SerializeField] private Vector3 originalScale;
    private Vector3 equipRotation;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void RecieveDamage(float damage)
    {
        damage = GameManager.Instance.normalEnemy.damageToObjects;
        durability -= damage;
    }
    /*
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
    */
}

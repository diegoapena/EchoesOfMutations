using System;
using UnityEngine;

[System.Serializable]
public class EquipablePair
{
    public BaseItemsData item;
    public GameObject ObjectOnScene;
}
public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;    
    [SerializeField]private EquipablePair[] equipables;
    [SerializeField]private BaseItemsData equippedItem;
    [SerializeField]private GameObject currentObjActive;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerController.OnScrollChanged += OnScroll ;
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnScroll(float direcction)
    {
        InventoryManager.Instance.ChangeSlot((int)direcction);
        Equip();
    }

    private void Equip()
    {
        Inventoryslot ActiveSlot = InventoryManager.Instance.GetActiveSlot();
        if (ActiveSlot == null || ActiveSlot.IsEmpty)
        {
            Unequip();
            return;
        }
        BaseItemsData items = ActiveSlot.Items;

        if(items.ItemTypes != ItemsTypes.Interactable && items.ItemTypes != ItemsTypes.Craftable)
        {
            Debug.Log(items.ItemName + "This item cannot be equipped");
            return;
        }
        if (equippedItem == items) return;

        GameObject ItemsFromObjects = SearchObject(items);

        if(ItemsFromObjects == null)
        {
            Debug.LogWarning(items.ItemName + " This item has not been assigned on EquipManager Pool's");
            return ;
        }

        if(currentObjActive != null)
        {
            currentObjActive.SetActive(false);

        }
        ItemsFromObjects.SetActive(true);
        currentObjActive = ItemsFromObjects;
        equippedItem = items;
        Debug.Log("Equipped : " + items.ItemName);

    }
    private GameObject SearchObject(BaseItemsData items)
    {
        for (int i = 0; i < equipables.Length; i++)
        {
            if (equipables[i].item == items)
            {
                return equipables[i].ObjectOnScene;
            }
        }
        return null;
    }
    private void Unequip()
    {
        if(currentObjActive != null)
        {
            currentObjActive.SetActive(false);  
        }
        currentObjActive = null;
        equippedItem = null;

    }

    private void DesActiveAllItems()
    {
        for (int i = 0; i < equipables.Length; i++)
        {
            if (equipables[i].ObjectOnScene != null)
            {
                equipables[i].ObjectOnScene.SetActive(false);
            }
        }      
    }
    public bool HasEquipped(BaseItemsData items)
    {
        return equippedItem == items;
    }
           
}

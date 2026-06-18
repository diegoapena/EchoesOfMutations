using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{   
    public static InventoryManager Instance;
    public CraftingStation Crafting;
    public const int MAX_SLOTS = 9;
    private DoubleLinkedList<Inventoryslot> list = new();
    public static event Action OnInventoryChanged;
    [FoldoutGroup("Craftable Settings")]
    private Dictionary<BaseMaterialData, int> materials = new();
    [FoldoutGroup("Craftable Settings")]
    public int CurrentWood;
    [FoldoutGroup("Craftable Settings")]
    public int CurrentMetal;
    [FoldoutGroup("Inventory Settings")]
    public int SlotActivo = 0;
    
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
            return;
        }
       
        
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    #region Craftable Methods
    [Button]
    public void AddMaterial(BaseMaterialData material , int amount)
    {
        if (materials.ContainsKey(material)) 
        {
            if (material.MaterialName == "Wood")
            {
                CurrentWood += amount;
            }
            else if (material.MaterialName == "Metal")
            {
                CurrentMetal += amount;               
            }           
            materials[material] += amount;
            Debug.Log("Material obtained :" + material.MaterialName + " - " + "Quantity :" + amount );           
        } 

        else 
            materials[material] = amount;
    }
    [Button]
    public bool CanCraft(ItemRecipe recipe)
    {
        foreach(var ingredient in recipe.Ingredients)
        {
            if(!materials.TryGetValue(ingredient.material , out int count) || count < ingredient.amount)
                return false;           
        }
        return true;
    }
    
    public bool Craft(ItemRecipe recipe , Vector3 spawnPosition)
    {
        if (!CanCraft(recipe)) 
        {         
            return false;
        }
        
        foreach(var ingredient in recipe.Ingredients)
        {
            materials[ingredient.material] -= ingredient.amount;           
        }       
        Instantiate(recipe.resultPrefab, spawnPosition, Quaternion.identity);
        return true;       
    }

    [Button]
    public int GetAmount(BaseMaterialData material) => materials.TryGetValue(material, out int count) ? count : 0;
    [Button]
    public void ClearInventory()
    {
        materials.Clear();
        CurrentMetal = 0;
        CurrentWood = 0;
    }
    #endregion

    #region Inventory Methods
    [Button]
    public bool Pickup(BaseItemsData items , int quantity = 1)
    {
        if(items == null) return false;
        Node<Inventoryslot> existingNode = list.Find(slot => slot.Items == items);

        if(existingNode != null && items.IsStackable)
        {
            int freeSpace = items.MaxStack - existingNode.Value.Quantity;

            if(freeSpace <= 0)
            {
                Debug.Log("Slot of " + items.ItemName + "full");
                return false;
            }
            existingNode.Value.Quantity += Mathf.Min(quantity, freeSpace);
            Debug.Log("Acumulate" + items.ItemName + " -  Item Quantity :" + existingNode.Value.Quantity);
        }
        else
        {
            if (list.Count >= MAX_SLOTS)
            {
                Debug.Log("Inventory full 9/9");
                return false;
            }

            list.AddLast(new Inventoryslot(items, quantity));
            Debug.Log(items.ItemName + "add. Slots : " + list.Count + " / " + MAX_SLOTS );
        }

        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(BaseItemsData items, int quantity = 1)
    {
        Node<Inventoryslot> node = list.Find(slot => slot.Items == items);
        if (node == null) return false;

        node.Value.Quantity -= quantity;

        if (node.Value.Quantity <= 0) 
        { 
            list.Remove(node);         
        }
        if(SlotActivo >= list.Count)
        {
            SlotActivo = Math.Max(0, list.Count - 1);
        }
        OnInventoryChanged?.Invoke();
        return true;
    }
    public Inventoryslot GetActiveSlot()
    {
        Inventoryslot[] dates = list.ToArray();
        if(dates.Length == 0) return null;
        if(SlotActivo >= dates.Length) return null;
        return dates[SlotActivo];      
    }
    [Button]
    public bool HasItem(BaseItemsData items)
    {
        Node<Inventoryslot> node = list.Find(searchItem);
        return node != null;

        bool searchItem(Inventoryslot slot)
        {
            return slot.Items == items;
        }
    }
    [Button]
    public int GetQuantity(BaseItemsData items)
    {
        Node<Inventoryslot> node = list.Find(searchItem);
        if(node != null)
        {
            return node.Value.Quantity;
        }
        else
        {
            return 0;
        }
        bool searchItem(Inventoryslot slot)
        {
            return slot.Items == items;
        }    
    }
    
    public Inventoryslot[] GetSlots() => list.ToArray();
    #endregion
    /*
    public void AddMaterial(BaseMaterialData material, int amount) => CraftingStation.AddMaterial(material, amount);
    */
}

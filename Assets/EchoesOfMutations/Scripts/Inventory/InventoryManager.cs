using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.InputSystem;
[Serializable]

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private CircularLinkedList<IInteractable> inventoryData = new();
 
    [FoldoutGroup("Inventory Settings")]
    public int MaxSlotsQuantity = 9;
    

    public static event Action<CircularLinkedList<IInteractable>> OnInventoryChanged;
    public static event Action<int> OnEquippedItem;
    private BaseInteractableObj currentEquipped;
    private Node<IInteractable> currentNode;
    private int selectedSlot = 0;

    [FoldoutGroup("Craftable Settings")]
    private Dictionary<BaseMaterialData, int> materials = new();
    [FoldoutGroup("Craftable Settings")]
    public int CurrentWood;
    [FoldoutGroup("Craftable Settings")]
    public int CurrentMetal;
    [FoldoutGroup("Craftable Settings")]
    public CraftingStation Crafting;
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

    private void OnEnable()
    {
        PlayerController.OnSlotSelected += SelectSlot;
        PlayerController.OnSlotScroll += ScrollSlot;
        PlayerController.OnRemoveItem += RemoveCurrentItem;
    }

    

    private void OnDisable()
    {
        PlayerController.OnSlotSelected -= SelectSlot;
        PlayerController.OnSlotScroll -= ScrollSlot;
        PlayerController.OnRemoveItem -= RemoveCurrentItem;
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
    public bool AddItem(IInteractable item)
    {
        if(inventoryData.Count >= MaxSlotsQuantity)
        {
            Debug.Log("Inventory full!");
            return false;
        }
        inventoryData.Add(item);
       Debug.Log(" Item added to "  + inventoryData.Count);
        currentNode = inventoryData.tail;
        selectedSlot = inventoryData.Count - 1;
        OnInventoryChanged?.Invoke(inventoryData);
        OnEquippedItem?.Invoke(selectedSlot);
        EquipNode(currentNode);
        return true;
    }
    private void RemoveCurrentItem()
    {
        if (currentNode != null)
            RemoveItem(currentNode.Value);
    }
    private bool RemoveItem(IInteractable item)
    {
        if (item == null) return false;
        if(inventoryData.Count == 0) return false;

        Node<IInteractable> found = null;
        Node<IInteractable> node = inventoryData.head;
        for (int i = 0; i < inventoryData.Count && node != null; i++) 
        { 
            if(node.Value == item)
            {
                found = node;
                break;
            }  
            node = node.Next;
        }
        if (found == null) return false;
        if(found == currentNode)
        {
            if(currentEquipped != null)
            {
                currentEquipped.OnUnEquipped();
                currentEquipped.OnPlaceItem(GameManager.Instance.playerManager.playerMechanics.ItemContainer.position);
                currentEquipped = null;
            }
        }
        Node<IInteractable> newCurrent = null;
        if (inventoryData.Count > 1)
        {
            newCurrent = (found == currentNode) ? found.Next : currentNode ?? inventoryData.head;
        }
        inventoryData.RemoveNode(found);
        currentNode = newCurrent;
        selectedSlot = currentNode != null? GetIndexByNode(currentNode) : -1;
        OnInventoryChanged?.Invoke(inventoryData);
        if (selectedSlot >= 0)
            OnEquippedItem?.Invoke(selectedSlot);
        return true;
         
    }
    private void SelectSlot(int index)
    {
        if(inventoryData.Count == 0 || index < 0 || index >= inventoryData.Count)
            return;
        selectedSlot = index;
        currentNode = GetNodeByIndex(index);
        Debug.Log("Selected slot :" + selectedSlot + 1);
        OnEquippedItem?.Invoke(selectedSlot);
        EquipNode(currentNode);
    }
    private void ScrollSlot(float direction)
    {
        if(inventoryData.Count == 0)
            return;
        if(currentNode == null)
            currentNode = inventoryData.head;

        currentNode = direction > 0 ? currentNode.Prev : currentNode.Next;

        selectedSlot = GetIndexByNode(currentNode);
        Debug.Log("Selected by scroll" + (selectedSlot + 1));
        OnEquippedItem?.Invoke(selectedSlot);
        EquipNode(currentNode);
    }
    private Node<IInteractable> GetNodeByIndex(int index)
    {
        Node<IInteractable> current = inventoryData.head;
        for (int i = 0; i < index && current != null; i++) 
            current = current.Next;
        return current;
    }
    private int GetIndexByNode(Node<IInteractable> target)
    {
        if (target == null)
            return -1;
        Node<IInteractable> current = inventoryData.head;
        for(int i = 0; i < inventoryData.Count; i++)
        {
            if (current == target)
                return i;

            current = current.Next;
        }
        return -1;
    }
    private void EquipNode(Node<IInteractable> node)
    {
        if (currentEquipped != null) 
        {
            currentEquipped.OnUnEquipped();
            currentEquipped = null;       
        }
        if(node?.Value is BaseInteractableObj item)
        {
            currentEquipped = item;
            currentEquipped.OnEquip(GameManager.Instance.playerManager.playerMechanics.ItemContainer);
            Debug.Log("Equipped :" + item.ItemData.ItemName);
        }
    }
    public IInteractable GetSelectedItem()
    {
        return currentNode?.Value;
    }
    #endregion
    /*
    public void AddMaterial(BaseMaterialData material, int amount) => CraftingStation.AddMaterial(material, amount);
    */
}

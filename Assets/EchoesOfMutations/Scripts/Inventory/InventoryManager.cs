using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
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
        PlayerController.OnSlotSelected += SelectedSlot;
        
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
    private void SelectedSlot()
    {
        
    }
    #endregion
    /*
    public void AddMaterial(BaseMaterialData material, int amount) => CraftingStation.AddMaterial(material, amount);
    */
}

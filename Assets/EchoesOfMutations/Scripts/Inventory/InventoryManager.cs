using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private Dictionary<BaseMaterialData, int> materials = new();
    public int CurrentWood;
    public int CurrentMetal;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
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
}

using Sirenix.OdinInspector;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public ItemRecipe[] availableRecipes;

    void Start()
    {
        
    }

    [Button]
    public void TryCraft(ItemRecipe recipe)
    {
        bool success = InventoryManager.Instance.Craft(recipe, transform.position);
        if (success == InventoryManager.Instance.Craft(recipe, transform.position))      
        {
            Debug.Log(recipe.ItemName + " : " + success + " -" + " Not enough materials ");
        }      
    }
}

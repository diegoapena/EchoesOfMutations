using Sirenix.OdinInspector;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public ItemRecipe[] availableRecipes;
    

    void Start()
    {
        
    }
    private void OnEnable()
    {
        PlayerController.OnCraftingOpen += OnCraftingOpenRequested;
    }
    private void OnDisable()
    {
        PlayerController.OnCraftingOpen -= OnCraftingOpenRequested;
    }

    private void OnCraftingOpenRequested(CraftingStation station)
    {
        if (station != this) return;

        if (GameManager.Instance.craftingUI.IsVisible)
        {
            GameManager.Instance.craftingUI.Hide();    
            
        }
        else
        {          
            GameManager.Instance.craftingUI.Show(availableRecipes, transform.position);           
        }
    }
}

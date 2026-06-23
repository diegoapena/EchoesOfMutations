using System.Text;
using TMPro;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlotUI : MonoBehaviour
{
    public Image recipeIcon;
    public TextMeshProUGUI recipeNameTxt;
    public TextMeshProUGUI ingredientsTxt;   
    public TextMeshProUGUI craftButtonTxt;    
    private ItemRecipe currentRecipe;

    void Start()
    {

    }
    
    

    
    void Update()
    {
        
    }
    public void SetUp(ItemRecipe recipe, CraftingUI ui)
    {
        currentRecipe = recipe;
        GameManager.Instance.craftingUI = ui;

      
        if (recipeNameTxt != null)
        {
            recipeNameTxt.text = recipe.ItemName;
        }     

        if (recipeIcon != null)
        {
            Sprite icon = GetSripteFromPrefab(recipe.resultPrefab);
            recipeIcon.sprite = icon;
            recipeIcon.enabled = icon != null;
        }
        if(ingredientsTxt != null)
        {
            ingredientsTxt.text = BuildIngredientText(recipe);
        }

        RefreshButtonState();
    }
    public void RefreshButtonState()
    {
        if (currentRecipe == null) return;

        bool canCraft = InventoryManager.Instance.CanCraft(currentRecipe);

        if(craftButtonTxt != null)
        {
            craftButtonTxt.text = (canCraft ? "Craft " : "Without materials");
        }
    }
    public void OnCraftClicked()
    {
        if(GameManager.Instance.craftingUI != null && currentRecipe != null)
        {
            GameManager.Instance.craftingUI.OnCraftPressed(currentRecipe);
        }
    }
    private string BuildIngredientText(ItemRecipe recipe)
    {
        if (recipe.Ingredients == null || recipe.Ingredients.Count == 0) return "No ingredients";

        StringBuilder sb = new();

        foreach (var ingredients in recipe.Ingredients) 
        {
            if(ingredients.material == null) continue;     
            int have = InventoryManager.Instance.GetAmount(ingredients.material);
            int need = ingredients.amount;
        }
        return sb.ToString().TrimEnd();
    }
    private Sprite GetSripteFromPrefab(GameObject prefab)
    {
        if(prefab == null) return null;
        SpriteRenderer sr = prefab.GetComponent<SpriteRenderer>();
        if (sr != null) return sr.sprite;

        Image img = prefab.GetComponent<Image>();
        if (img != null) return img.sprite;
        return null;

    }
}

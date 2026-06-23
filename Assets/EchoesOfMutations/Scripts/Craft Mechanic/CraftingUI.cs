using UnityEngine;
public class CraftingUI : MonoBehaviour
{
    public GameObject craftingPanel;
    public Transform recipeContainer;
    public GameObject recipeSlotPrefab;
    

    private Vector3 stationPosition;

    public bool IsVisible => craftingPanel != null && craftingPanel.activeSelf;


    private void Awake()
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


    void Start()
    {
        Hide();
    }

    
    void Update()
    {
        
    }
    private void OnCraftingOpenRequested(CraftingStation station) { } 
    public void Show(ItemRecipe[] recipes, Vector3 spawnPosition)
    {
        stationPosition = spawnPosition;
        craftingPanel.SetActive(true);
        BuildRecipeList(recipes);   
    }
    public void Hide()
    {
        if(craftingPanel != null)
        {
            craftingPanel.SetActive(false);
        }
        ClearRecipeList();
        
    }
    private void BuildRecipeList(ItemRecipe[] recipes)
    {
        ClearRecipeList();
        if (recipes == null) return;

        foreach(ItemRecipe recipe in recipes)
        {
            if(recipe == null) continue;

            GameObject slot = Instantiate(recipeSlotPrefab, recipeContainer);

            RecipeSlotUI slotUI = slot.GetComponent<RecipeSlotUI>();

            if(slotUI != null)
            {
                slotUI.SetUp(recipe, this);
            }
            else
            {
                Debug.LogWarning("RecipeSlotPrefab does not have a RecipeSlotUI component");
            }
        }
    }
    private void ClearRecipeList()
    {
        if (recipeContainer == null) return;
        foreach(Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnCraftPressed(ItemRecipe recipe)
    {
        bool sucess = InventoryManager.Instance.Craft(recipe, stationPosition);

        if (sucess)
        {
            Debug.Log("Crafted :" + recipe.ItemName);

            foreach (Transform child in recipeContainer)

                child.GetComponent<RecipeSlotUI>()?.RefreshButtonState();
        }
        else
        {
            Debug.Log("Not enough materials for : " + recipe.ItemName);
        }
    }


}

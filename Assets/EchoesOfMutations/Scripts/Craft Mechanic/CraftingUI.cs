using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public class CraftingUI : MonoBehaviour
{
    public GameObject craftingPanel;
    public Transform recipeContainer;
    public GameObject recipeSlotPrefab;
    

    private Vector3 stationPosition;

    [FoldoutGroup("Craft Text References")]
    public TextMeshProUGUI CurrentMaterialTxt_1;
    [FoldoutGroup("Craft Text References")]
    public TextMeshProUGUI CurrentMaterialTxt_2;
    [FoldoutGroup("Craft Text References")]
    public TextMeshProUGUI CurrentMaterialTxt_3;
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
        UpdateMaterials();
    }
    
    public void Show(ItemRecipe[] recipes, Vector3 spawnPosition)
    {
        stationPosition = spawnPosition;
        craftingPanel.SetActive(true);
        BuildRecipeList(recipes);   
    }
    public bool IsVisible => craftingPanel != null && craftingPanel.activeSelf;
    public void Hide()
    {
        if(craftingPanel != null)
        {
            craftingPanel.SetActive(false);
            Cursor.visible = false;
        }
        ClearRecipeList();       
    }
    private void OnCraftingOpenRequested(CraftingStation station) { }
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
    public void UpdateMaterials()
    {
        CurrentMaterialTxt_1.text = " Current Wood: " + InventoryManager.Instance.CurrentWood;
        CurrentMaterialTxt_2 .text = " Current Metal: " + InventoryManager.Instance.CurrentMetal;
        CurrentMaterialTxt_3.text = " Current Scrap: " + InventoryManager.Instance.CurrentScrap;
    }


}

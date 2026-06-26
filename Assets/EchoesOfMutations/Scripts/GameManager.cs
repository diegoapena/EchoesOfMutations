using Sirenix.OdinInspector;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [FoldoutGroup("References")]
    public PlayerManager playerManager;
    [FoldoutGroup("References")]
    public Animator animationManager;
    [FoldoutGroup("References")]
    public InventoryManager invetoryManager;
    [FoldoutGroup("References")]
    public WaveManager waveManager;
    [FoldoutGroup("References")]
    public HitScan hitscan;


    [FoldoutGroup("References/Items")]
    public FlashLight flashLight;
    [FoldoutGroup("References/Items")]
    public Barricade barricade;
    [FoldoutGroup("References/Items")]
    public Gun gun;

    [FoldoutGroup("References/BaseEnemy")]
    public NormalEnemy normalEnemy;
    [FoldoutGroup("References/BaseEnemy")]
    public StrongEnemy strongEnemy;
    [FoldoutGroup("References/Cinematic")]
    public CarfinalCamera carfinalCamera;
    [FoldoutGroup("References/Cinematic")]
    public Car car;
    [FoldoutGroup("References/Cinematic")]
    public TestCinematicMutant mutantCinematic;
    [FoldoutGroup("References/Cinematic")]
    public Collition collition;

    [FoldoutGroup("UI")]
    public RecipeSlotUI recipeSlotUI;
    [FoldoutGroup("UI")]
    public CraftingUI craftingUI;
    [FoldoutGroup("UI")]
    public CraftingStation craftingStation;
   private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
 
    void Update()
    {
        
    }
}

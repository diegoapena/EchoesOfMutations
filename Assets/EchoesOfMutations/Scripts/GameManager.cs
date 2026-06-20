using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerManager playerManager;
    public Animator animationManager;
    public InventoryManager invetoryManager;

    [FoldoutGroup("Items")]
    public FlashLight flashLight;
    [FoldoutGroup("Items")]
    public Barricade barricade;
    [FoldoutGroup("Items")]
    public Gun gun;

    [FoldoutGroup("BaseEnemy")]
    public NormalEnemy normalEnemy;
    
    [FoldoutGroup("Cinematic")]
    public Car car;
    [FoldoutGroup("Cinematic")]
    public TestCinematicMutant mutantCinematic;
    [FoldoutGroup("Cinematic")]
    public Collition collition;
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

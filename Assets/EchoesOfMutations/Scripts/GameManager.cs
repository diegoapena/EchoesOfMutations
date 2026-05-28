using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerManager playerManager;

    //          IN TEST
    public FlashLight flashLight;
    //

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

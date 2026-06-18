using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;
    [SerializeField] private Transform handPoint;
    private BaseItemsData equippedItem;
    private GameObject currentItemPrefab;

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
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

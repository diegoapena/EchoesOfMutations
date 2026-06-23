using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemiesData", menuName = "EchoesOfMutations/BaseEnemiesData")]
public class BaseEnemiesData : ScriptableObject
{
    [FoldoutGroup("Settings")]
    [SerializeField] private int id;
    [FoldoutGroup("Settings")]
    [SerializeField] private string enemyName;
    [FoldoutGroup("Settings")]
    [SerializeField] private int damageQuantity;
    [FoldoutGroup("Settings")]
    [SerializeField] private int amount = 1;
    [FoldoutGroup("Settings"), PreviewField(150)]
    public GameObject enemyPrefab;
    [FoldoutGroup("Settings"), PreviewField(150)]
    public Sprite enemyIcon;
    [FoldoutGroup("Settings"), TextArea(2,10)]
    [SerializeField] private string enemyDescription;

    public int Id => id;
    public string EnemyName => enemyName;
    public int DamageQuantity => damageQuantity;
    public int Amount => amount;    
    public string EnemyDescription => enemyDescription;
}

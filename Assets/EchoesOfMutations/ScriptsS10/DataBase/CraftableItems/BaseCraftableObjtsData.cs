using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCraftableObjtsData", menuName = "EchoesOfMutations/BaseCraftableObjtsData")]
public class BaseCraftableObjtsData : ScriptableObject
{
    [FoldoutGroup("Settings")]
    [SerializeField] private int id;
    [FoldoutGroup("Settings")]
    [SerializeField] private string itemName;
    [FoldoutGroup("Settings")]
    [SerializeField] private int itemCost;
    [FoldoutGroup("Settings")]
    [SerializeField] private int itemAmount;

    public int ID => id;
    public string ItemName => itemName;
    public int ItemCost => itemCost;
    public int ItemAmount => itemAmount;

}

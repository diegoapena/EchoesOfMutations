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
    [SerializeField] private int woodCost;
    [FoldoutGroup("Settings")]
    [SerializeField] private int metalCost;

    public int ID => id;
    public string ItemName => itemName;
    public int WoodCost => woodCost;
    public int MetalCost => metalCost;

}

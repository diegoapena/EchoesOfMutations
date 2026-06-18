using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "DataBaseItems", menuName = "EchoesOfMutations/DataBaseItems")]
public class DataBaseItems : SerializedScriptableObject
{
    [FoldoutGroup("References"),PreviewField(150)]
    public GameObject itemPrefab;

    public Dictionary<ItemsTypes, List<BaseItemsData>> itemDataBase = new();
    public BaseItemsData GetItem(ItemsTypes itemType, string itemName)
    {
        if (itemDataBase.TryGetValue(itemType, out List<BaseItemsData> items))
        {
            return items.Find(item => item.ItemName == itemName);
        }
        else
        {
            throw new System.Exception("Item type not found in database: " + itemType);
        }
    }   
}

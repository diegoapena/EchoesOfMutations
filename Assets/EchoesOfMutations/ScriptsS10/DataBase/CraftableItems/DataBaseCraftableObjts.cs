using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBaseCraftableObjts", menuName = "EchoesOfMutations/DataBaseCraftableObjts")]
public class DataBaseCraftableObjts : SerializedScriptableObject
{
    public Dictionary<int, List<BaseCraftableObjtsData>> itemcraftableData = new();

    public BaseCraftableObjtsData GetCraftableObjt(int itemCost , string itemName)
    {
        if(itemcraftableData.TryGetValue(itemCost , out List<BaseCraftableObjtsData> itemcrafable))
        { 
            
            return itemcrafable.Find(itemCraftable =>  itemCraftable.ItemCost == itemCost);
        }
        else
        {
            // still in coding
            return null; 
        }
    }
    
}

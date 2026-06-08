using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBaseCraftableObjts", menuName = "EchoesOfMutations/DataBaseCraftableObjts")]
public class DataBaseCraftableObjts : SerializedScriptableObject
{
    public Dictionary<string, List<BaseCraftableObjtsData>> itemcraftableData = new();

    public BaseCraftableObjtsData GetCraftableObjt(int woodCost , int metalCost , string itemName)
    {
        if(itemcraftableData.TryGetValue(itemName, out List<BaseCraftableObjtsData> itemcrafable))
        { 
            
            return itemcrafable.Find(itemCraftable =>  itemCraftable.WoodCost == woodCost && itemCraftable.MetalCost == metalCost);
        }
        else
        {
            throw new System.Exception("Crafable type not found in database: " + itemName);           
        }
    }
    
}

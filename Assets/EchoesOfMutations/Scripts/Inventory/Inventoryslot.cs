using UnityEngine;

public class Inventoryslot 
{
    public BaseItemsData Items;
    public int Quantity;    
    
    public Inventoryslot(BaseItemsData items, int quantity )
    {
        Items = items;
        Quantity = quantity;
    }
    public bool IsEmpty => Items == null;
}

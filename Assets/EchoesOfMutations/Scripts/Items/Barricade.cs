using System;
using UnityEngine;

public class Barricade : BaseCraftable 
{

    public event Action<int> OnHit;

    void Start()
    {
        
    }

    
    void Update()
    {
        OnDestroy();
    }
    public void OnDestroy()
    {
        if (gameObject == null) return;
        if(durability<= 0)
        {      
            Destroy(gameObject);
        }
    }

}

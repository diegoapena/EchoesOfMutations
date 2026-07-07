using NUnit.Framework.Interfaces;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Barricade : BaseCraftable 
{    
    public event Action<int> OnHit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        DestroyObj();
    }
    public void DestroyObj()
    {
        if (gameObject == null) return;
        if(durability<= 0)
        {      
            Destroy(gameObject);
        }
    }    
}

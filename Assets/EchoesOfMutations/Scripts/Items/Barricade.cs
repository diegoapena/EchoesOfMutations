using UnityEngine;

public class Barricade : BaseCraftable 
{
    

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

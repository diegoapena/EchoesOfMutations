using UnityEngine;

public class BaseCraftable : MonoBehaviour , IDamageable
{
    public float durability;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void RecieveDamage(float damage)
    {
        damage = GameManager.Instance.normalEnemy.damageToObjects;
        durability -= damage;
    }
}

using UnityEngine;


public class HitScan : MonoBehaviour
{   
    public float DamageHit;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void DamageToEnemy(GameObject target)
    {
        if (target.TryGetComponent(out IDamageable damageable))
        {
            damageable.RecieveDamage(DamageHit);
        }
    }
    public void MakeDamage(GameObject target)
    {
        DamageToEnemy(target);
    }
}

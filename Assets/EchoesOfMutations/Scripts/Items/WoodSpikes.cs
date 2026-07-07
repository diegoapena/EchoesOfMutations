using UnityEngine;

public class WoodSpikes : BaseCraftable
{
    public float damage = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        DestroyObj();
    }
    public void DamageToEnemy(GameObject obj)
    {
        if (obj.TryGetComponent(out IDamageable damageable))
        {
            damageable.RecieveDamage(damage);
        }
    }
    public void MakeDamage(GameObject obj)
    {
        DamageToEnemy(obj);
    }
    public void DestroyObj()
    {
        if (gameObject == null) return;
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
           other.GetComponent<BaseEnemy>()?.AttackObj(gameObject);
        }
    }
}

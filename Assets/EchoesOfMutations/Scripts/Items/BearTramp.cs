using UnityEngine;

public class BearTramp : BaseCraftable
{
    public float damage = 10f;
    [SerializeField] private float stunDuration = 2f;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }
    void Update()
    {
        ActiveGravity();
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

    public void ActiveGravity()
    {
        if (!isInInventory)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        else
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

}



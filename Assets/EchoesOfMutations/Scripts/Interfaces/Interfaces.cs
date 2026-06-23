using UnityEngine;

public interface IInteractable
{
    public void Interact();   
}
public interface IDamageable
{
    public void RecieveDamage(float damage);
}


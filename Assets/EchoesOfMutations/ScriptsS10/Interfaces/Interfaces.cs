using UnityEngine;

public interface IInteractable
{
    public void Interact(Transform interactor);
}
public interface IDamageable
{
    public void TakeDamage(float damage);
}


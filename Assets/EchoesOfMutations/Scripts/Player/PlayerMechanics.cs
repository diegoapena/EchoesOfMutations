using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMechanics : MonoBehaviour , IDamageable
{
    public Transform ItemContainer;
    public float PlayerLife = 100f;

    public event Action OnPlayerDead;
    private GameObject currentItem;
    
    void Update()
    {

    }
    public void RecieveDamage(float damage)
    {
        PlayerLife -= damage;
    }    
    public void Dead()
    {       
        if (PlayerLife <= 0)
        {          
            OnPlayerDead?.Invoke();
            Debug.Log("You're dead! X_X");         
            Destroy(gameObject);
        }
    }


    
}
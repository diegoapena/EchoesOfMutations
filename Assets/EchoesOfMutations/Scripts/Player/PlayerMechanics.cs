using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMechanics : MonoBehaviour , IDamageable
{
    public Transform ItemContainer;
    public float PlayerLife = 100f;

    public event Action OnPlayerDead;
    private GameObject currentItem;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        OnPlayerDead += Dead;
    }   
    private void OnDisable()
    {
        OnPlayerDead -= Dead;
    }
    void Update()
    {

    }
    public void RecieveDamage(float damage)
    {
        PlayerLife -= damage;
    }    
    public void Dead()
    {
        OnPlayerDead?.Invoke();
        if (PlayerLife <= 0)
        {          
            Debug.Log("You're dead! X_X");         
            Destroy(gameObject);
        }
    }


    
}
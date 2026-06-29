using Sirenix.OdinInspector;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : BaseEnemy
{    
    public Action OnDead;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void OnEnable()
    {
        BaseEnemy.OnDeath += OnDead;
    }
    private void OnDisable()
    {
        BaseEnemy.OnDeath -= OnDead;
    }
    void Start()
    {


    }
    void Update()
    {       
        DeadEnemy();     
        NextTarget();
        Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barricade"))
        {
            CurrentBarricade = other.gameObject.GetComponent<Barricade>();
            barricades.Add(other.gameObject.GetComponent<Barricade>());                             
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barricade"))
        {
            FindBarricade();         
        }       
    }
    
    
    private void DeadEnemy()
    {
        if (health <= 0)
        {
            OnDead?.Invoke();
            Debug.Log("dead");
            if(GetComponent<NavMeshAgent>().enabled == true)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
                GetComponent<NavMeshAgent>().enabled = false;
            }           
            Destroy(gameObject,1f);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.3f);
        
        Gizmos.color = Color.blueViolet;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}

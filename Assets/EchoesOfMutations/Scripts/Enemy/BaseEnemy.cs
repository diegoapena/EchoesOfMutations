using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemy : MonoBehaviour,IDamageable
{
    [SerializeField] private BaseEnemiesData enemyData;
    [FoldoutGroup ("References")]
    public NavMeshAgent agent;
    
    [FoldoutGroup("Attack Settings")]
    public bool isAttacking = false;
    [FoldoutGroup("Attack Settings")]
    public float CurrentAttackCD;
    [FoldoutGroup("Attack Settings")]
    public float AttackInterval;
    [FoldoutGroup("Attack Settings")]
    public float damageToObjects;

    [FoldoutGroup("Health Settings")]
    public float health;

    public static event Action OnDeath;
    public Barricade CurrentBarricade;
    public List<Barricade> barricades;
    public LayerMask Barricades;
    private void Awake()
    {    
    }

    void Start()
    {       
    }
   
    void Update()
    {
      
    }
    /*
    public void FindBarricades()
    {
        if (CurrentBarricade != null) return;
        Collider[] colls = Physics.OverlapSphere(transform.position, 4, Barricades);

        Barricade nearestBarricade = null;

        Vector3 pos = transform.position;

        foreach (var col in colls)
        {
            
            if (Vector3.Distance(pos, col.transform.position) <= Vector3.Distance(pos, nearestBarricade.transform.position))
            {
                nearestBarricade = col.GetComponent<Barricade>();
                Debug.Log(col.gameObject.name);
                               
                
                
            }
            else
            {
                //barricades.Remove(col.gameObject.GetComponent<Barricade>());
            }
        }
        
        isAttacking = true;
    }

    */

    public void Attack()
    {
        
        if (isAttacking)
        {
            if(Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hit, 0.7f, Barricades))
            {
                GameObject obj = hit.collider.gameObject;
                CurrentBarricade.RecieveDamage(2);
                isAttacking = false;
                Debug.Log(hit.collider.name);
            }
        }
        else
        {
            StartCoroutine(nameof(EnableAttack));
        }
            /* 
             if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hit, 0.5f, Barricades))
             {
                 if (hit.collider.gameObject)
                 {
                     StartCoroutine(nameof(EnableAttack));
                 }
                 //isAttacking = true;

                 Debug.Log(hit.collider.name);


                 GameObject obj = hit.collider.gameObject;
                 if (hit.collider.gameObject)
                 {

                 }
                 CurrentBarricade.RecieveDamage(2);


                 if (hit.collider.gameObject == null) return;

             }
             */     
    }
    public void Target()
    {
        if (!agent.hasPath)
        {
            Debug.Log("No path to follow");
        }
        if (GameManager.Instance.playerManager != null && GetComponent<NavMeshAgent>().enabled == true)
        {               

            OnDeath?.Invoke();
            /*
            if (health <= 0 && GetComponent<NavMeshAgent>().enabled == true )
            {
                Debug.Log("dead 2"); 
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
                GetComponent<NavMeshAgent>().enabled = false;

                return;
            }  
            */
            agent.SetDestination(GameManager.Instance.playerManager.transform.position);
            agent.stoppingDistance = 1.5f;
        }
        
    }
    public void FindBarricade()
    {
        ClearBarricades();
        if (CurrentBarricade == null && barricades.Count > 0) 
        {
            Barricade nearestBarricade = barricades[0];
            
            Vector3 pos = transform.position;

            foreach (Barricade barricade in barricades) 
            { 
                if(Vector3.Distance(pos , barricade.transform.position) < Vector3.Distance(pos , nearestBarricade.transform.position))
                {                  
                    nearestBarricade = barricade;
                }
                CurrentBarricade = nearestBarricade;
            }        
        }       
    }
    public void NextTarget()
    {
        if (CurrentBarricade != null)
        {
            agent.SetDestination(CurrentBarricade.transform.position);
            agent.stoppingDistance = 2f;
        }
        else
        {
            Target();
        }
    }
    public IEnumerator EnableAttack()
    {        
        
        CurrentAttackCD = 0;
        while(CurrentAttackCD <= AttackInterval)
        {
            CurrentAttackCD += Time.deltaTime;
            yield return null;
        }        
        isAttacking = true;       
        yield break;
        
    }

    public void RecieveDamage(float damage)
    {
        damage = GameManager.Instance.hitscan.DamageHit;
        health-=damage;
    }
    public void ClearBarricades()
    {
        barricades.RemoveAll( x => x == null);
    }
}

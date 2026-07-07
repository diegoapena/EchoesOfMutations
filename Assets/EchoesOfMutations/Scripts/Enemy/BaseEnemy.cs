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
    [FoldoutGroup("Attack Settings")]
    public float damageToPlayer;
    [FoldoutGroup("Health Settings")]
    public float health;
    public static event Action OnDeath;
    public Barricade CurrentBarricade;
    public List<Barricade> barricades;

    public EnemiesTypes enemyType;

    public LayerMask Barricades;
    public LayerMask playerLayer;
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
            if(Physics.SphereCast(transform.position, 1.3f, transform.forward, out RaycastHit hit, 1.5f, Barricades))
            {
                GameObject obj = hit.collider.gameObject;               
                AttackObj(CurrentBarricade.gameObject);
                isAttacking = false;
                Debug.Log(hit.collider.name);
            }

            
        }
        else
        {
            StartCoroutine(nameof(EnableAttack));
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, 1.3f, playerLayer);
        foreach (Collider col in hits)
        {
            col.GetComponent<IDamageable>()?.RecieveDamage(damageToPlayer);           
            Debug.Log(col.name);
        }
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
        if(CurrentBarricade == null)
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
    public void DamageObject(GameObject target)
    {
        if (target.TryGetComponent(out IDamageable damageable))
        {
            damageable.RecieveDamage(damageToObjects);
        }
    }
    public void AttackObj(GameObject target)
    {
        DamageObject(target);
    }
    public void RecieveDamage(float damage)
    {      
        health-=damage;
    }
    public void ClearBarricades()
    {
        barricades.RemoveAll( x => x == null);
    }    
}

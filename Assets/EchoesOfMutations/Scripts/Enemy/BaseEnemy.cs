using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{
    [FoldoutGroup ("References")]
    public NavMeshAgent agent;
    

    [FoldoutGroup("Attack Settings")]
    public float damageToObjects;
    [FoldoutGroup("Attack Settings")]
    public bool isAttacking = false;
    [FoldoutGroup("Attack Settings")]
    public float CurrentAttackCD;
    [FoldoutGroup("Attack Settings")]
    public float AttackInterval;

    public Barricade CurrentBarricade = null;
    public List<Barricade> barricades;
    public LayerMask LayerBarricades;
    private void Awake()
    {
        
       
       // CurrentBarricade =  GetComponent<GameObject>();
    }

    void Start()
    {       
    }
   
    void Update()
    {
        
    }
    public void FindBarricades()
    {
        if (CurrentBarricade != null) return;
        Collider[] colls = Physics.OverlapSphere(transform.position, 4, LayerBarricades);

        Barricade nearestBarricade = null;

        Vector3 pos = transform.position;

        foreach (var col in colls)
        {
            if (Vector3.Distance(pos, col.transform.position) < Vector3.Distance(pos, nearestBarricade.transform.position))
            {
                nearestBarricade = col.GetComponent<Barricade>();
            }
        }
        CurrentBarricade = nearestBarricade;
        isAttacking = true;
    }


    public void Attack()
    {
        if (isAttacking)
        {
            if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit, 1, LayerBarricades))
            {
                Debug.DrawRay(transform.position,transform.forward * hit.distance, Color.black);

                if (hit.collider.gameObject == null) return;

                GameObject obj = hit.collider.gameObject;
                Debug.Log(hit.collider.name);
                isAttacking = false;

                CurrentBarricade.RecieveDamage(2);
                StartCoroutine(nameof(EnableAttack));                
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1, Color.red);
        }
    }
    public void ChangeTarget()
    {
        if (!agent.hasPath)
        {
            Debug.Log("No path to follow");
        }
        if (GameManager.Instance.playerManager != null)
        {
            agent.SetDestination(GameManager.Instance.playerManager.transform.position);
            agent.stoppingDistance = 1;
        }
    }
    public void FindBarricade()
    {
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
            }      
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
}

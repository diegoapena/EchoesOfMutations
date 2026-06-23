using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class StrongEnemy : BaseEnemy
{
    [FoldoutGroup("Attack Settings")]
    public float damageToObjects;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        NextTarget();
        Attack();
    }

    public void NextTarget()
    {
        if (CurrentBarricade == null)
        {
            ChangeTarget();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barricade"))
        {
            CurrentBarricade = other.gameObject.GetComponent<Barricade>();
            barricades.Add(other.gameObject.GetComponent<Barricade>());
            FindBarricade();

        }
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barricade"))
        {

            if (CurrentBarricade != null)
            {
                agent.SetDestination(CurrentBarricade.transform.position);
                agent.stoppingDistance = 2;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Barricade"))
        {
            barricades.Remove(other.gameObject.GetComponent<Barricade>());
        }
    }
}

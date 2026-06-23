using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : BaseEnemy
{

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
    public void NextTarget()
    {
        if (CurrentBarricade == null)
        {
            ChangeTarget();
        }
    }
}

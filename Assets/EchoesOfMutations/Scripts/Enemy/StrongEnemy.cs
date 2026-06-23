using UnityEngine;
using UnityEngine.AI;

public class StrongEnemy : BaseEnemy
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
    public void NextTarget()
    {
        if (CurrentBarricade == null)
        {
            ChangeTarget();
        }
    }
}

using UnityEngine;

public class NormalEnemy : BaseEnemy
{

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
            barricades.Add(other.gameObject.GetComponent<Barricade>());
            CurrentBarricade = other.gameObject;
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

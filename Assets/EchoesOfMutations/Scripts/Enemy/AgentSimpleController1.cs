using UnityEngine;
using UnityEngine.AI;
public class AgentSimpleController : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Asignar autom�ticamente el Target al Transform del jugador
        if (Target == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                Target = playerObject.transform;
            }
            else
            {
                Debug.LogError("No se encontr� un objeto con la etiqueta 'Player'. Aseg�rate de que el jugador tenga la etiqueta correcta.");
            }
        }
    }

    void Update()
    {
        if (agent != null && Target != null)
        {
            agent.SetDestination(Target.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Target != null)
        {
            Vector3 dir = Target.position - transform.position;
            Gizmos.DrawRay(transform.position, dir);
        }
    }
}

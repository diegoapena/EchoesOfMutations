    using UnityEngine;
using MoreMountains.Feedbacks;
using System.Collections;

public class WoodSpawner : MonoBehaviour
{
    public MMF_Player WoodMove; 
    public GameObject woodPrefab; 
    public Transform spawnPoint; 
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Box"))
        {
           
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; 
            }

            
            Destroy(other.gameObject);

            StartCoroutine(SpawnWoodObjects());
        }
    }

    private IEnumerator SpawnWoodObjects()
    {
      
        for (int i = 0; i < 3; i++)
        {
           
            GameObject wood = Instantiate(woodPrefab, spawnPoint.position, spawnPoint.rotation);

            
            MMF_Player feedback = wood.GetComponent<MMF_Player>();
            if (feedback != null)
            {
                feedback.PlayFeedbacks();
            }

            yield return new WaitForSeconds(5f);
        }
    }
}

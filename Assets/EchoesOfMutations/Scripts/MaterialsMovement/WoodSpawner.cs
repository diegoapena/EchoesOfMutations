    using UnityEngine;

using System.Collections;

public class WoodSpawner : MonoBehaviour
{
    
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

            yield return new WaitForSeconds(5f);
        }
    }
}

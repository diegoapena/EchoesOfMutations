    using UnityEngine;

public class WoodDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject woodPrefab; 
    [SerializeField] private Transform spawnPoint; 
    private int currentWoodCount = 3; 
    private const int maxWoodCount = 3; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Wood"))
        {
          
            Destroy(other.gameObject);

            
            currentWoodCount--;

            
            if (currentWoodCount < maxWoodCount)
            {
                SpawnWood();
            }
        }
    }

    private void SpawnWood()
    {
       
        Instantiate(woodPrefab, spawnPoint.position, spawnPoint.rotation);

        
        currentWoodCount++;
    }
}

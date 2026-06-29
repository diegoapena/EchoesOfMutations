using UnityEngine;

public class MetalDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject MetalPrefab;
    [SerializeField] private Transform spawnPoint;
    private int currentMetalCount = 3;
    private const int maxMetalCount = 3;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Metal"))
        {

            Destroy(other.gameObject);


            currentMetalCount--;


            if (currentMetalCount < maxMetalCount)
            {
                SpawnMetal();
            }
        }
    }

    private void SpawnMetal()
    {

        Instantiate(MetalPrefab, spawnPoint.position, spawnPoint.rotation);


        currentMetalCount++;
    }
}

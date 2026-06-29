using UnityEngine;

using System.Collections;

public class MetalSpawner : MonoBehaviour
{

    public GameObject MetalPrefab;
    public Transform spawnPoint;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MetalBox"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }


            Destroy(other.gameObject);

            StartCoroutine(SpawnMetalObjects());
        }
    }

    private IEnumerator SpawnMetalObjects()
    {

        for (int i = 0; i < 3; i++)
        {

            GameObject wood = Instantiate(MetalPrefab, spawnPoint.position, spawnPoint.rotation);

            yield return new WaitForSeconds(5f);
        }
    }
}
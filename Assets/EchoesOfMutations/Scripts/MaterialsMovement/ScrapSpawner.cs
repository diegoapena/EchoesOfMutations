using System.Collections;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject materialPrefab;
    [SerializeField] private Transform InitialSpawnPoint;   
    [SerializeField] private float WaitTime;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ScrapBox"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
             
        }
        Destroy(other.gameObject);
        StartCoroutine(nameof(SpawnMaterial));
    }

    private IEnumerator SpawnMaterial()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(materialPrefab, InitialSpawnPoint.transform.position , InitialSpawnPoint.rotation);
            yield return new WaitForSeconds(WaitTime);
        }
        yield break;
    }
}

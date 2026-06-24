using UnityEngine;

public class Collition : MonoBehaviour
{
    
   
    private void OnEnable()
    {
        TestCinematicMutant.OnMoveMutant += GameManager.Instance.mutantCinematic.MoveMutant;
    }
    private void OnDisable()
    {
        TestCinematicMutant.OnMoveMutant -= GameManager.Instance.mutantCinematic.MoveMutant;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            GameManager.Instance.mutantCinematic.gameObject.SetActive(true);
            GameManager.Instance.car.speed = 10;           
            TestCinematicMutant.OnMoveMutant?.Invoke();          
            Debug.Log("Collition Detected");
        }
    }
}

using UnityEngine;

public class Collition : MonoBehaviour
{
    //THIS SCRIP IS FOR TESTING PURPOSES ONLY (TEMPORALY)
    public TestCinematicMutant mutant;
    public Car car;
    private void OnEnable()
    {
        TestCinematicMutant.OnMoveMutant += mutant.MoveMutant;
    }
    private void OnDisable()
    {
        TestCinematicMutant.OnMoveMutant -= mutant.MoveMutant;
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
            mutant.gameObject.SetActive(true);
            car.speed = 0;
            TestCinematicMutant.OnMoveMutant?.Invoke();
            Debug.Log("Collition Detected");
        }
    }
}

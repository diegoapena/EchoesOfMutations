using UnityEngine;

public class Car : MonoBehaviour
{
    //THIS SCRIP IS FOR TESTING PURPOSES ONLY (TEMPORALY)
    public float speed;
    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 Dir = Vector3.left * speed * Time.deltaTime;
        transform.position -= Dir;
    }
}

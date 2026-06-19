using UnityEngine;

public class Car : MonoBehaviour
{
    
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
        Vector3 Dir = Vector3.right * speed * Time.deltaTime;
        transform.position -= Dir;
    }
}

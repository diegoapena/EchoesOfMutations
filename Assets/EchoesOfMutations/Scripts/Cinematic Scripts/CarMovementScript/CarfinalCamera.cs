using UnityEngine;

using System.Collections;

public class CarfinalCamera : MonoBehaviour
{
    
    public float speed;
    void Start()
    {
        
    }
    public void Update()
    {
        Move();
    }



    public void Move()
    {
        StartCoroutine(WaitAndGoCar());
    }


    private IEnumerator WaitAndGoCar()
    {
        yield return new WaitForSeconds(10f);

        Vector3 Dir = Vector3.back * speed * Time.deltaTime;
        transform.position -= Dir;

    }
}

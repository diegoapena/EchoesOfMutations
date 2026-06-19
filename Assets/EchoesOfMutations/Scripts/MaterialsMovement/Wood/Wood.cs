using UnityEngine;



public class Wood : MonoBehaviour
{
    [SerializeField] private float speed = 3f; 
    [SerializeField] private float duration = 5f; 

    private float elapsedTime = 0f; 

    void Update()
    {
        
        if (elapsedTime < duration)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
    }
}
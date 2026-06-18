using UnityEngine;



public class Wood : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Velocidad del movimiento
    [SerializeField] private float duration = 5f; // Duración del movimiento

    private float elapsedTime = 0f; // Tiempo transcurrido

    void Update()
    {
        // Mover el objeto hacia adelante mientras no se exceda la duración
        if (elapsedTime < duration)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
    }
}
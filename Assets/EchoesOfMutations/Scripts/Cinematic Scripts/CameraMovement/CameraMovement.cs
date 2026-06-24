using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class CameraMovement : MonoBehaviour
{
    public float speed;
    public GameObject panel
        ;
    void Start()
    {

    }


    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 Dir = Vector3.back * speed * Time.deltaTime;
        transform.position -= Dir;
        StartCoroutine(WaitASec());
    }
    public void ActivarPanel()
    {
        panel.SetActive(false);
    }




    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2f);
        speed = 0;
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);

    }
}

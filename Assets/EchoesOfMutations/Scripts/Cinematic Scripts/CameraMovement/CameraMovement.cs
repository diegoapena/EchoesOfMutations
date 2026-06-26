using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.UI;


public class CameraMovement : MonoBehaviour
{
    public float speed;
    public GameObject panel;
    public CinemachineCamera CameraPoint1;
    public CinemachineCamera CameraPoint2;
    public CinemachineCamera CameraPoint3;
    public Light spotlight1;
    public Light spotlight2;
    private Image panelImage;

    // Variables para el fade
    private float fadeDuration = 1f;
    private float fadeElapsed = 0f;

    void Start()
    {
        CameraPoint1.Priority = 1;
        CameraPoint2.Priority = 0;
        CameraPoint3.Priority = 0;

        panelImage = panel.GetComponent<Image>();
        fadeElapsed = 0f; 
        StartCoroutine(FadeOutCoroutine());
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

    // Ahora no recibe parámetros
    

    // Tampoco recibe parámetros
    private IEnumerator FadeOutCoroutine()
    {
        // Asegura que el panel empiece completamente opaco
        Color color = panelImage.color;
        color.a = 1f;
        panelImage.color = color;

        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
            color.a = newAlpha;
            panelImage.color = color;
            yield return null;
        }
        // Asegura que el alfa quede exactamente en 0 al final
        color.a = 0f;
        panelImage.color = color;
    }

    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2f);
        speed = 0;
        CameraPoint1.Priority = 0;
        CameraPoint2.Priority = 1;
        CameraPoint3.Priority = 0;
        yield return new WaitForSeconds(1f);
    }
}

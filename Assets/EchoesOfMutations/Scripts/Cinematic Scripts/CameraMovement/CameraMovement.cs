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
        
        StartCoroutine(Fade(1f, 0f));
        StartCoroutine(WaitASec());
    }
    public void Update()
    {
        Move();
    }
    public void Move()
    {
        if (speed > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }


    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        fadeElapsed = 0f;
        Color color = panelImage.color;
        color.a = startAlpha;
        panelImage.color = color;

        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, fadeElapsed / fadeDuration);
            color.a = newAlpha;
            panelImage.color = color;
            yield return null;
        }
       
        color.a = endAlpha;
        panelImage.color = color;
    }



    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2f);
        speed = 0;
        CameraPoint1.Priority = 0;
        CameraPoint2.Priority = 1;
        CameraPoint3.Priority = 0;
    
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(Fade(0f, 1f));
        fadeElapsed = 0f;

        yield return new WaitForSeconds(1f);
        CameraPoint1.Priority = 0;
        CameraPoint2.Priority = 0;
        CameraPoint3.Priority = 1;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Fade(1f, 0));
        fadeElapsed = 0f;

        GameManager.Instance.carfinalCamera.Move();



    }
}


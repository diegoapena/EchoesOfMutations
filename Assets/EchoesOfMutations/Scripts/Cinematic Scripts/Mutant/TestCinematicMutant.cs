using DG.Tweening;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestCinematicMutant : MonoBehaviour
{
  
    public float MutantSpeed; 
    public static Action OnMoveMutant;
    public GameObject OptionsUI;
    public CinemachineCamera CarCamera;
    public CinemachineCamera CarCrashCamera;
    public CinemachineCamera DummyAnimationCamera;
    public Animator AnimacionDummy;

    void Start()
    {
        OptionsUI.SetActive(false);
        CarCamera.Priority = 1;
        AnimacionDummy.Play("IdleDummy");
    }

    
    void Update()
    {
        
    }
    public void ActivarPanel()
    {
        OptionsUI.SetActive(true);
    }
    
    public void MoveMutant()
    {              
        Vector3 moveDir = Vector3.right * MutantSpeed * Time.deltaTime;
        transform.position -= moveDir;
        StartCoroutine(nameof(WaitAndActivatePanel));
    }

    private IEnumerator WaitAndActivatePanel()
    {
        yield return new WaitForSeconds(1f);
        ActivarPanel();


        yield return new WaitForSeconds(1f);
        CarCrashCamera.Priority = 1;
        DummyAnimationCamera.Priority = 0;
        CarCamera.Priority = 0;


        yield return new WaitForSeconds(3f);
        OptionsUI.SetActive(false);


        yield return new WaitForSeconds(6f);
        DummyAnimationCamera.Priority = 1;
        CarCamera.Priority = 0;
        CarCrashCamera.Priority = 0;
        AnimacionDummy.Play("DummyAnimation");


        yield return new WaitForSeconds(5f);
        OptionsUI.SetActive(true);
        SceneManager.LoadScene("EchoesOfMutationsGameplay");
    }
    


}

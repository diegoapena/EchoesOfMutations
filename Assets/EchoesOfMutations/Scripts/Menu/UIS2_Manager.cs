using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class UIS2_Manager : MonoBehaviour
{
    [FoldoutGroup("Pause")]
    public GameObject panelPauseUI; 
    [FoldoutGroup("Pause")]
    private bool isPaused = false;
    [FoldoutGroup("Pause")]
    private InputSystem_Actions inputs; 

    private void Awake()
    {
        inputs = new(); 
        inputs.UI.Cancel.performed += OnPausePressed; 
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable(); 
    }
   
    public void ExitToPause()
    {
        
        panelPauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    public void GoToPause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panelPauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }
    
    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            ExitToPause();
        }
        else
        {
            GoToPause();
            
        }
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("EMMenu");
    }
}

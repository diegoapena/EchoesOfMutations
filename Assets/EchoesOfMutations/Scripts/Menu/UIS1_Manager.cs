using UnityEngine;
using UnityEngine.SceneManagement;

public class UIS1_Manager : MonoBehaviour
{
    public GameObject OptionsUI;
    private bool isPaused = false;
  
    public void ExitToOptions_Menu()
    {
        OptionsUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void GoToOptions_Menu()
    {
        OptionsUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
}


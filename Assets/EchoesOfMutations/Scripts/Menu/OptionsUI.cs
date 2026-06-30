using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUI : MonoBehaviour
{
    public GameObject Options;
    private bool isPaused = false;
  
    public void ExitToOptions_Menu()
    {
        Options.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void GoToOptions_Menu()
    {
        Options.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
}


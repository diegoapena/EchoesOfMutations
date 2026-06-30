using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public OptionsUI optionsUI;
    public GameUI gameUI;
    public StatsUI statsUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject);       
        }
    }
    public void Jugar()
    {
        SceneManager.LoadScene("PrincipalCinematic");
    }
    public void Exit()
    {
        Application.Quit();
        
    }
    public void GoToEntities()
    {
        SceneManager.LoadScene("");
    }
    
}

    
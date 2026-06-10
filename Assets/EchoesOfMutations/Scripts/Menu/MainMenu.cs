using UnityEngine;
using UnityEngine.SceneManagement;

// Este script controla el menú principal del juego.
// Permite iniciar el juego o salir de la aplicación.
// Relación con otros scripts:
// No tiene una relación directa con otros scripts, pero controla el flujo inicial del juego.
public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Application.Quit();
        
    }
    public void GoToEntities()
    {
        SceneManager.LoadScene("Entities");
    }
}

    
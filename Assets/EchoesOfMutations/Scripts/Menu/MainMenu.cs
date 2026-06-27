using UnityEngine;
using UnityEngine.SceneManagement;

// Este script controla el men� principal del juego.
// Permite iniciar el juego o salir de la aplicaci�n.
// Relaci�n con otros scripts:
// No tiene una relaci�n directa con otros scripts, pero controla el flujo inicial del juego.
public class MainMenu : MonoBehaviour
{
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

    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeMenu : MonoBehaviour
{
    public void CargarEscenaMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void CargarEscenaJuego()
    {
        SceneManager.LoadScene("Juego");
    }

    public void CargarEscenaTaller()
    {
        SceneManager.LoadScene("Taller");
    }

    public void CargarEscenaEstadisticas()
    {
        SceneManager.LoadScene("Estadisticas");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void BorrarPreferencias()
    {
        PlayerPrefs.DeleteAll();
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeVida : MonoBehaviour
{
    //Gestion de vida
    int saludJugadorInicial = 100;
    int saludJugador;
    bool impactoRecibido = false;
    private ControladorDeJuego controladorDeJuego;

    public Slider barraDeSalud;
    public Image ImagenImpacto;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f);

    // Use this for initialization
    void Start ()
    {
        GameObject gameControlerObject = GameObject.FindWithTag("GameController");
        if (gameControlerObject != null)
        {
            controladorDeJuego = gameControlerObject.GetComponent<ControladorDeJuego>();
        }
        if (controladorDeJuego == null)
        {
            Debug.Log("No se ha encontrado 'ControladorDeJuego' script");
        }

        saludJugador = saludJugadorInicial;
        actualizarBarra();
    }
	
	// Update is called once per frame
	void Update ()
    {
        gestionarImpacto();
    }

    private void gestionarImpacto()
    {
        if (impactoRecibido)
        {
            ImagenImpacto.color = flashColor;
        }
        else
        {
            ImagenImpacto.color = Color.Lerp(ImagenImpacto.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        impactoRecibido = false;
    }

    public bool Impacto(int daño)
    {
        impactoRecibido = true; //Activo el flag de impacto
        saludJugador -= daño;
        actualizarBarra();
        if (saludJugador <= 0) //Si se queda sin vida...
        {
            controladorDeJuego.GameOver();
            return true;
        }
        return false;
    }

    public void AumentarVida(int cantidad)
    {
        if (saludJugador <= 100)
        {
            saludJugador = Math.Min(100, saludJugador + cantidad); //Maximo 100
            actualizarBarra();
            controladorDeJuego.Notificar("Salud +" + cantidad);
        }
        else
        {
            controladorDeJuego.AddScore(cantidad);
            controladorDeJuego.Notificar("Puntos +" + cantidad);
        }
    }

    /// <summary>
    /// Informa de la vida actual
    /// </summary>
    /// <returns></returns>
    public int VidaActual()
    {
        return saludJugador;
    }

    /// <summary>
    /// Actualiza la barra de energia
    /// </summary>
    private void actualizarBarra()
    {
        if (barraDeSalud != null)
        {
            barraDeSalud.value = saludJugador;
        }
    }
}

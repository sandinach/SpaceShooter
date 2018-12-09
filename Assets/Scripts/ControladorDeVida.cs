using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeVida : MonoBehaviour
{
    //Gestion de vida
    int saludJugadorInicial = 100;
    int saludJugador;
    bool impactoRecibido = false;
    private ControladorDeJuego controladorDeJuego;

    public Slider barra;
    public TextMeshProUGUI textoValor;
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
        actualizarCapaVisual();
        StartCoroutine(regenerar());
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
        saludJugador = Math.Max(0, saludJugador); //Nunca menos de 0
        actualizarCapaVisual();
        if (saludJugador <= 0) //Si se queda sin vida...
        {
            //Para que no termine (ocasionalmente) de color rojo
            ImagenImpacto.color = Color.clear;

            controladorDeJuego.GameOver();
            return true;
        }
        return false;
    }

    public void AumentarVida(int cantidad)
    {
        if (saludJugador < 100)
        {
            saludJugador = Math.Min(100, saludJugador + cantidad); //Maximo 100
            actualizarCapaVisual();
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
    private void actualizarCapaVisual()
    {
        if (barra != null)
        {
            barra.value = saludJugador;
        }
        if (textoValor != null)
        {
            textoValor.SetText(saludJugador + "%");
        }
    }

    /// <summary>
    /// Rutina de regeneración
    /// </summary>
    /// <returns></returns>
    IEnumerator regenerar()
    {
        while (true)
        { // loops forever...
            if (saludJugador < 100)
            { // Si está por debajo de 100
                saludJugador += 1; //La vida la incrementamos de 1 en 1 
                saludJugador = Math.Min(100, saludJugador); //Máximo 100
                actualizarCapaVisual();
                yield return new WaitForSeconds(1);
            }
            else
            { // Si ya esta a tope, no hago nada 
                yield return null;
            }
        }
    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase para controlar la energia
/// </summary>
public class ControladorDeEnergia : MonoBehaviour
{
    private int energia = 0;
    private ControladorDeJuego controladorDeJuego;

    public Slider barra;
    public TextMeshProUGUI textoValor;

    void Start()
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

        energia = (int) CONFIGURACION.GetEnergiaInicial();
        actualizarCapaVisual();
        StartCoroutine(generarEnergia());
    }

    public bool ObtenerEnergia(int cantidadRequerida)
    {
        if (energia >= cantidadRequerida)
        {
            energia -= cantidadRequerida;
            actualizarCapaVisual();
            return true;
        }
        return false; //No hay suficiente energia
    }

    public void AumentarEnergia(int cantidad)
    {
        if (energia < 100)
        {
            energia = Math.Min(100, energia + cantidad); //Maximo 100
            actualizarCapaVisual();
            controladorDeJuego.Notificar("Energía +" + cantidad);
        }
        else
        {
            controladorDeJuego.AddScore(cantidad);
            controladorDeJuego.Notificar("Puntos +" + cantidad);
        }
    }

    /// <summary>
    /// Informa de la energia actual
    /// </summary>
    /// <returns></returns>
    public int EnergiaActual()
    {
        return energia;
    }

    /// <summary>
    /// Actualiza la barra de energia
    /// </summary>
    private void actualizarCapaVisual()
    {
        if (barra != null)
        {
            barra.value = energia;
        }
        if(textoValor != null)
        {
            textoValor.SetText(energia + "%");
        }
    }

    /// <summary>
    /// Rutina de regeneración
    /// </summary>
    /// <returns></returns>
    IEnumerator generarEnergia()
    {
        while (true)
        { // loops forever...
            if (energia < 100)
            { // Si está por debajo de 100
                energia += (int) CONFIGURACION.GetGeneracionEnergia(); // Obtenemos el valor de incremento 
                energia = Math.Min(100, energia); //Máximo 100
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

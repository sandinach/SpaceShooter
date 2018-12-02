using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase para controlar la energia
/// </summary>
public class ControladorDeEnergia : MonoBehaviour
{
    private int energia = 0;

    public Slider barraEnergia;

    void Start()
    {
        energia = (int) CONFIGURACION.GetEnergiaInicial();
        actualizarBarra();
        StartCoroutine(generarEnergia());
    }

    public bool ObtenerEnergia(int cantidadRequerida)
    {
        if (energia >= cantidadRequerida)
        {
            energia -= cantidadRequerida;
            actualizarBarra();
            return true;
        }
        return false; //No hay suficiente energia
    }

    /// <summary>
    /// Actualiza la barra de energia
    /// </summary>
    private void actualizarBarra()
    {
        if (barraEnergia != null)
        {
            barraEnergia.value = energia;
        }
    }

    /// <summary>
    /// Rutina de generación de energia
    /// </summary>
    /// <returns></returns>
    IEnumerator generarEnergia()
    {
        while (true)
        { // loops forever...
            if (energia < 100)
            { // if health < 100...
                energia += (int) CONFIGURACION.GetGeneracionEnergia(); // increase health and wait the specified time
                actualizarBarra();
                yield return new WaitForSeconds(1);
            }
            else
            { // if health >= 100, just yield 
                yield return null;
            }
        }
    }
}

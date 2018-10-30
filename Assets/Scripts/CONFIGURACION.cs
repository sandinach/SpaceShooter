using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONFIGURACION
{
    //Parametros de inicialización
    private static SliderConfigInt VELOCIDAD_ENEMIGOS = new SliderConfigInt(-4, -5, -8);
    private static SliderConfigInt ENEMIGOS_POR_OLEADA = new SliderConfigInt(5, 8, 20); //Establezco un rango de 5 a 20 con valor actual de 8

    /// <summary>
    /// Devuelve el valor de configuración o la preferencia de usuario para los enemigos por oleada
    /// </summary>
    /// <returns>Valor configurado y si no tiene el valor por defecto</returns>
    public static int GetEnemigosPorOleada()
    {
        return PlayerPrefs.GetInt("EnemigosPorOleada", ENEMIGOS_POR_OLEADA.GetCurrent());
    }

    public static SliderConfigInt GetConfigEnemigosPorOleada()
    {
        return ENEMIGOS_POR_OLEADA;
    }

    /// <summary>
    /// Devuelve el valor de configuración o la preferencia de usuario para la velocidad de los enemigos
    /// </summary>
    /// <returns>Valor configurado y si no tiene el valor por defecto</returns>
    public static int GetVelocidadEnemigos()
    {
        return PlayerPrefs.GetInt("VelocidadEnemigos", VELOCIDAD_ENEMIGOS.GetCurrent());
    }

    public static SliderConfigInt GetConfigVelocidadEnemigos()
    {
        return VELOCIDAD_ENEMIGOS;
    }

    public static void SetEnemigosPorOleada(int numeroDeEnemigos)
    {
        PlayerPrefs.SetInt("EnemigosPorOleada", numeroDeEnemigos);
    }

    public static void SetVelocidadEnemigos(int velocidadEnemigos)
    {
        PlayerPrefs.SetInt("VelocidadEnemigos", velocidadEnemigos);
    }
}

/// <summary>
/// Clase para la configuración de sliders de tipo entero
/// </summary>
public class SliderConfigInt
{
    int valorMinimo;
    int valorMaximo;
    int valorActual;

    private SliderConfigInt() { } //Nada de contructor vacío

    public SliderConfigInt(int min, int current, int max)
    {
        valorMinimo = min;
        valorActual = current;
        valorMaximo = max;
    }

    /// <summary>
    /// Obtiene el valor actual
    /// </summary>
    /// <returns></returns>
    public int GetCurrent()
    {
        return valorActual;
    }

    /// <summary>
    /// Obtiene el valor mínimo
    /// </summary>
    /// <returns></returns>
    public int GetMin()
    {
        return valorMinimo;
    }

    /// <summary>
    /// Obtiene el valor máximo
    /// </summary>
    /// <returns></returns>
    public int GetMax()
    {
        return valorMaximo;
    }


    /// <summary>
    /// Establece el valor actual
    /// </summary>
    /// <param name="newVal"></param>
    public void SetCurrent(int newVal)
    {
        //si esta en rango
        if (newVal.CompareTo(valorMinimo) <= 0 && newVal.CompareTo(valorMaximo) <= 0)
            valorActual = newVal;
    }
}





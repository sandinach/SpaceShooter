using UnityEngine;

public static class CONFIGURACION
{
    public static readonly float TIEMPO_NOTIFICACIONES = 1f; //Un segundo

    //Parametros de inicialización
    private static SliderConfig VELOCIDAD_ENEMIGOS = new SliderConfig(4, 5, 8);
    private static SliderConfig ENEMIGOS_POR_OLEADA = new SliderConfig(5, 8, 20); //Establezco un rango de 5 a 20 con valor actual de 8

    //Gestion de energia
    private static SliderConfig GENERACION_ENERGIA = new SliderConfig(1, 2, 3);
    private static SliderConfig ENERGIA_INICIAL = new SliderConfig(0, 50, 100);

    /// <summary>
    /// Devuelve el valor de configuración o la preferencia de usuario para los enemigos por oleada
    /// </summary>
    /// <returns>Valor configurado y si no tiene el valor por defecto</returns>
    public static float GetEnemigosPorOleada()
    {
        float preferencia = PlayerPrefs.GetFloat("EnemigosPorOleada", ENEMIGOS_POR_OLEADA.GetCurrent());
        ENEMIGOS_POR_OLEADA.SetCurrent(preferencia); //Asigna si esta en rango
        return ENEMIGOS_POR_OLEADA.GetCurrent();
    }

    public static SliderConfig GetConfigEnemigosPorOleada()
    {
        return ENEMIGOS_POR_OLEADA;
    }

    public static float GetEnergiaInicial()
    {
        float preferencia = PlayerPrefs.GetFloat("EnergiaInicial", ENERGIA_INICIAL.GetCurrent());
        ENERGIA_INICIAL.SetCurrent(preferencia); //Asigna si esta en rango
        return ENERGIA_INICIAL.GetCurrent();
    }

    public static SliderConfig GetConfigEnergiaInicial()
    {
        return ENERGIA_INICIAL;
    }

    public static float GetGeneracionEnergia()
    {
        float preferencia = PlayerPrefs.GetFloat("GeneracionEnergia", GENERACION_ENERGIA.GetCurrent());
        GENERACION_ENERGIA.SetCurrent(preferencia); //Asigna si esta en rango
        return GENERACION_ENERGIA.GetCurrent();
    }

    public static SliderConfig GetConfigGeneracionEnergia()
    {
        return GENERACION_ENERGIA;
    }

    /// <summary>
    /// Devuelve el valor de configuración o la preferencia de usuario para la velocidad de los enemigos
    /// </summary>
    /// <returns>Valor configurado y si no tiene el valor por defecto</returns>
    public static float GetVelocidadEnemigos()
    {
        return PlayerPrefs.GetFloat("VelocidadEnemigos", VELOCIDAD_ENEMIGOS.GetCurrent());
    }

    public static SliderConfig GetConfigVelocidadEnemigos()
    {
        return VELOCIDAD_ENEMIGOS;
    }

    public static void SetEnemigosPorOleada(float numeroDeEnemigos)
    {
        if (GetEnemigosPorOleada() != numeroDeEnemigos)
        {
            PlayerPrefs.SetFloat("EnemigosPorOleada", numeroDeEnemigos);
            ENEMIGOS_POR_OLEADA.SetCurrent(numeroDeEnemigos);
        }
    }

    public static void SetVelocidadEnemigos(float velocidadEnemigos)
    {
        if (GetVelocidadEnemigos() != velocidadEnemigos)
        {
            PlayerPrefs.SetFloat("VelocidadEnemigos", velocidadEnemigos);
            VELOCIDAD_ENEMIGOS.SetCurrent(velocidadEnemigos);
        }
    }

    public static void SetGeneracionEnergia(float valor)
    {
        if (GetGeneracionEnergia() != valor)
        {
            PlayerPrefs.SetFloat("GeneracionEnergia", valor);
            GENERACION_ENERGIA.SetCurrent(valor);
        }
    }

    public static void SetEnergiaInicial(float valor)
    {
        if (GetEnergiaInicial() != valor)
        {
            PlayerPrefs.SetFloat("EnergiaInicial", valor);
            ENERGIA_INICIAL.SetCurrent(valor);
        }
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
        if (newVal.CompareTo(valorMinimo) >= 0 && newVal.CompareTo(valorMaximo) <= 0)
            valorActual = newVal;
        else
            Debug.Log("SliderConfigInt.SetCurrent => Valor ignorado: " + newVal);
    }
}

/// <summary>
/// Clase para la configuración de sliders de tipo entero
/// </summary>
public class SliderConfig
{
    float valorMinimo;
    float valorMaximo;
    float valorActual;

    private SliderConfig() { } //Nada de contructor vacío

    public SliderConfig(float min, float current, float max)
    {
        valorMinimo = min;
        valorActual = current;
        valorMaximo = max;
    }

    /// <summary>
    /// Obtiene el valor actual
    /// </summary>
    /// <returns></returns>
    public float GetCurrent()
    {
        return valorActual;
    }

    /// <summary>
    /// Obtiene el valor mínimo
    /// </summary>
    /// <returns></returns>
    public float GetMin()
    {
        return valorMinimo;
    }

    /// <summary>
    /// Obtiene el valor máximo
    /// </summary>
    /// <returns></returns>
    public float GetMax()
    {
        return valorMaximo;
    }


    /// <summary>
    /// Establece el valor actual
    /// </summary>
    /// <param name="newVal"></param>
    public void SetCurrent(float newVal)
    {
        //si esta en rango
        if (newVal.CompareTo(valorMinimo) >= 0 && newVal.CompareTo(valorMaximo) <= 0)
            valorActual = newVal;
        else
            Debug.Log("SliderConfig.SetCurrent => Valor ignorado: " + newVal);
    }
}







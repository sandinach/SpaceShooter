using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeDificultad
{
    private NivelDificultad currentLevel;

    // Use this for initialization
    public ControladorDeDificultad()
    {
        LoadLevels();
	}

    /// <summary>
    /// Carga la configuración de niveles
    /// </summary>
    private void LoadLevels()
    {
        currentLevel = new NivelDificultad(0, 8, 1.5f, -6f, true); //test values
    }

    /// <summary>
    /// Devuelve la instancia del nivel de dificultad actual
    /// </summary>
    /// <returns></returns>
    public NivelDificultad GetCurrentLevel()
    {
        return currentLevel;
    }

    /// <summary>
    /// Actualiza la puntuación por si hay que cambiar de nivel
    /// </summary>
    /// <param name="puntuacion">Nueva puntuación del jugador</param>
    public void Update(int puntuacion)
    {

    }
}

/// <summary>
/// Clase para almacenar los modificadores de dificultad
/// </summary>
public class NivelDificultad
{
    public int NumeroDeNivel = 1;
    public int Enemigos = 0;
    public float Cadencia = 0f;
    public float Velocidad = 0f;
    public bool UsarMovimientoEvasivo = false;

    /// <summary>
    /// Constructor
    /// </summary>
    public NivelDificultad()
    {

    }

    /// <summary>
    /// Constructor
    /// </summary>
    public NivelDificultad(int numero, int enemigos, float cadencia, float velocidad, bool maniobras)
    {
        NumeroDeNivel = numero;
        Enemigos = enemigos;
        Cadencia = cadencia;
        Velocidad = velocidad;
        UsarMovimientoEvasivo = maniobras;
    }
}

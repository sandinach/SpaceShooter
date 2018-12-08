using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeDificultad
{
    private NivelDificultad currentLevel;
    private IEnumerator<NivelDificultad> enumeradorNiveles;
    private INotificador notificador;
    private int siguienteNivel = 100;
    private bool nivelMaximoAlcanzado= false;

    // Use this for initialization
    public ControladorDeDificultad(INotificador toNotificador)
    {
        notificador = toNotificador;
        LoadLevels();
	}

    /// <summary>
    /// Carga la configuración de niveles
    /// </summary>
    private void LoadLevels()
    {
        DefaultLevels();
    }

    /// <summary>
    /// Carga la configuración de niveles
    /// </summary>
    private void DefaultLevels()
    {
        List <NivelDificultad> listaNiveles = new List<NivelDificultad>();
        listaNiveles.Add(new NivelDificultad("Nivel 1", 1, 1.5f, -6f, true, 200));
        listaNiveles.Add(new NivelDificultad("Nivel 2", 2, 1.4f, -7f, true, 300));
        listaNiveles.Add(new NivelDificultad("Nivel 3", 3, 1.3f, -8f, true, 400));
        listaNiveles.Add(new NivelDificultad("Nivel 4", 4, 1.2f, -9f, true, 500));
        listaNiveles.Add(new NivelDificultad("Nivel 5", 5, 1.1f, -10f, true, 600));
        listaNiveles.Add(new NivelDificultad("Nivel 6", 6, 1.0f, -10f, true, 200));
        listaNiveles.Add(new NivelDificultad("Nivel 7", 7, 0.9f, -11f, true, 200));
        listaNiveles.Add(new NivelDificultad("Nivel 8", 8, 0.8f, -12f, true, 200));
        listaNiveles.Add(new NivelDificultad("Nivel 9", 9, 0.7f, -13f, true, 200));
        listaNiveles.Add(new NivelDificultad("Nivel 10", 10, 0.6f, -14f, true, 200));

        enumeradorNiveles = listaNiveles.GetEnumerator();
        if(enumeradorNiveles.MoveNext())
        {
            EstablecerNuevoNivel();
        }
    }

    /// <summary>
    /// Devuelve la instancia del nivel de dificultad actual
    /// </summary>
    /// <returns></returns>
    public NivelDificultad GetNivelActual()
    {
        return currentLevel;
    }

    /// <summary>
    /// Actualiza la puntuación por si hay que cambiar de nivel
    /// </summary>
    /// <param name="puntuacion">Nueva puntuación del jugador</param>
    public void Update(int puntuacion)
    {
        siguienteNivel -= puntuacion;
        if (siguienteNivel <= 0 && !nivelMaximoAlcanzado)
        {   
            //Test values
            if(enumeradorNiveles.MoveNext())
            {
                EstablecerNuevoNivel();
            }
            else
            {
                nivelMaximoAlcanzado = true;
            }
        }
    }

    private void EstablecerNuevoNivel()
    {
        currentLevel = enumeradorNiveles.Current;
        siguienteNivel = currentLevel.SiguienteNivel;
        notificador.Notificar(currentLevel.NombreNivel);
        Debug.Log("Nivel establecido: " + currentLevel.NombreNivel);
    }

    public string GetNombreNivel()
    {
        return currentLevel.NombreNivel;
    }
}

/// <summary>
/// Clase para almacenar los modificadores de dificultad
/// </summary>
public class NivelDificultad
{
    public string NombreNivel = "Nivel 1";
    public int Enemigos = 0;
    public float Cadencia = 0f;
    public float Velocidad = 0f;
    public bool UsarMovimientoEvasivo = false;
    public int SiguienteNivel = 0;

    /// <summary>
    /// Constructor
    /// </summary>
    public NivelDificultad()
    {

    }

    /// <summary>
    /// Constructor
    /// </summary>
    public NivelDificultad(string nombre, int enemigos, float cadencia, float velocidad, bool maniobras, int puntosSiguienteNivel)
    {
        NombreNivel = nombre;
        Enemigos = enemigos;
        Cadencia = cadencia;
        Velocidad = velocidad;
        UsarMovimientoEvasivo = maniobras;
        SiguienteNivel = puntosSiguienteNivel;
    }
}

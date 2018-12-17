using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Clase para el control de niveles de dificultad
/// Gestiona la carga de niveles JSON o defecto y el cambio de nivel por puntuación
/// </summary>
public class ControladorDeDificultad
{
    private NivelDificultad currentLevel;
    private IEnumerator<NivelDificultad> enumeradorNiveles;
    private INotificador notificador;
    private int siguienteNivel;
    private bool nivelMaximoAlcanzado= false;

    private readonly string nombreFicheroDeNiveles = "niveles.json";

    #region Constructores

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="toNotificador">Referencia al notificador</param>
    public ControladorDeDificultad(INotificador toNotificador)
    {
        notificador = toNotificador;
        LoadLevels();
	}

    #endregion Constructores

    /// <summary>
    /// Carga la configuración de niveles
    /// </summary>
    private void LoadLevels()
    {
        if (!LoadLevelsFromFile()) //Trato de cargar los niveles desde fichero
        {
            DefaultLevels(); //Sino utilizo los valores HardCoded
        }
    }

    private bool LoadLevelsFromFile()
    {
        string rutaFichero = Path.Combine(Application.streamingAssetsPath, nombreFicheroDeNiveles);
        if (File.Exists(rutaFichero))
        {
            string nivelesAsJson = File.ReadAllText(rutaFichero);
            NivelesDeDificultad niveles = JsonUtility.FromJson<NivelesDeDificultad>(nivelesAsJson);
            if (niveles != null && niveles.Niveles.Count > 0) //Verifico que tenga niveles
            {
                enumeradorNiveles = niveles.Niveles.GetEnumerator();
                if (enumeradorNiveles.MoveNext())
                {
                    EstablecerNuevoNivel();
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Carga la configuración de niveles
    /// </summary>
    private void DefaultLevels()
    {
        List<NivelDificultad> listaNiveles = new List<NivelDificultad>
        {
            new NivelDificultad("Nivel 1", 0, 1.5f, -6f, true, 300),
            new NivelDificultad("Nivel 2", 4, 1.5f, -6f, true, 400),
            new NivelDificultad("Nivel 3", 4, 1.4f, -6f, true, 500),
            new NivelDificultad("Nivel 4", 6, 1.4f, -6f, true, 600),
            new NivelDificultad("Nivel 5", 6, 1.3f, -7f, true, 700),
            new NivelDificultad("Nivel 6", 10, 1.3f, -8f, true, 800),
            new NivelDificultad("Nivel 7", 10, 1.3f, -9f, true, 900),
            new NivelDificultad("Nivel 8", 14, 1.0f, -10f, true, 1000),
            new NivelDificultad("Nivel 9", 14, 1.0f, -11f, true, 1100),
            new NivelDificultad("Nivel Máximo", 18, 1.0f, -12f, true, 1200)
        };

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
        //Debug.Log("Nivel establecido: " + currentLevel.NombreNivel);
    }

    public string GetNombreNivel()
    {
        return currentLevel.NombreNivel;
    }
}

[System.Serializable]
public class NivelesDeDificultad
{
    public List<NivelDificultad> Niveles = new List<NivelDificultad>();

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public NivelesDeDificultad() { }
}

/// <summary>
/// Clase para almacenar los modificadores de dificultad
/// </summary>
[System.Serializable]
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

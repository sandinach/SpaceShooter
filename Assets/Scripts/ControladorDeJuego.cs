using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControladorDeJuego : MonoBehaviour, INotificador
{
    public GameObject[] tiposDeEnemigos;
    public Vector3 zonaDespliegeEnemigos;
    public GameObject[] tiposDePowerUps;

    /// <summary>
    /// Pausa entre enemigo y enemigo dentro de la oleada
    /// </summary>
    public float pausaEntreEnemigos;

    /// <summary>
    /// Pausa inicial
    /// </summary>
    public float pausaInicial;
    /// <summary>
    /// Pausa entre oleadas
    /// </summary>
    public float pausaEntreOleadas;

    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoEstado;
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoNotificaciones;
    public GameObject MenuOpciones;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool pausa;
    private int numeroEnemigos;

    private ControladorDeDificultad levelManager = null;

    private PuntuacionMaxima puntuacionMaxima;

    private readonly string TEXTO_GAMEOVER = "Game Over!";

    void Start()
    {
        levelManager = new ControladorDeDificultad(this);
        pausa = false;
        score = 0;
        restart = false;
        gameOver = false;

        //Valor inicial para el número de enemigos
        numeroEnemigos = (int) CONFIGURACION.GetEnemigosPorOleada();

        textoEstado.SetText("");
        textoNotificaciones.SetText("");
        MenuOpciones.SetActive(false);
        UpdateScore();

        //Cargamos la puntuación del jugador
        CargarPuntuacionJugador();

        //Empezamos
        StartCoroutine(SpawnWaves());
    }

    #region Puntuacion máxima

    /// <summary>
    /// Determina si la puntuación actual es la mejor del jugador
    /// </summary>
    /// <param name="puntuacion"></param>
    private void NuevaPuntuacion(int puntuacion)
    {
        if(puntuacion > puntuacionMaxima.PuntosMaximos)
        {
            GuardarPuntuacionJugador();
            Notificar("Nuevo record!", 5f);
        }
    }

    /// <summary>
    /// Recupera la puntuación máxima del jugador
    /// </summary>
    private void CargarPuntuacionJugador()
    {
        puntuacionMaxima = new PuntuacionMaxima();
        if(PlayerPrefs.HasKey("puntuacionMaxima"))
        {
            puntuacionMaxima.PuntosMaximos = PlayerPrefs.GetInt("puntuacionMaxima");
        }
    }

    /// <summary>
    /// Guarda en preferencias la puntuación del jugador
    /// </summary>
    private void GuardarPuntuacionJugador()
    {
        PlayerPrefs.SetInt("puntuacionMaxima", score);
    }

    #endregion Puntuacion máxima

    public void GameOver()
    {
        //Game over
        textoEstado.SetText(TEXTO_GAMEOVER);
        gameOver = true;

        //Evaluamos si ha hecho record
        NuevaPuntuacion(score);

        //El menú lo mostramos al terminar la oleada
    }

    public void RestartLevel()
    {
        if (restart)
            SceneManager.LoadScene("Juego");
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        levelManager.Update(newScoreValue); //Actualizo el controlador de niveles
        UpdateScore();
    }

    /// <summary>
    /// Obtiene el nivel de juego actual (se usa durante el proceso de configuración de enemigos)
    /// </summary>
    /// <returns></returns>
    public NivelDificultad GetNivelDificultad()
    {
        return levelManager.GetNivelActual();
    }

    private void UpdateScore()
    {
        textoPuntuacion.SetText("Puntos: " + score);
        textoNivel.SetText(levelManager.GetNombreNivel());
    }

    private void Update()
    {
        gestionarPausa();

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Juego");
            }
            MenuOpciones.SetActive(true);
        }
    }

    private void gestionarPausa()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pausa = !pausa;
        }

        if(pausa)
        {
            Time.timeScale = 0;
            textoEstado.SetText("Partida en pausa");
        }
        else
        {
            Time.timeScale = 1;
            if (!gameOver)
                textoEstado.SetText("");
            else
                textoEstado.SetText(TEXTO_GAMEOVER);
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(pausaInicial);
            for (int i = 0; i < CalcularNumeroEnemigos(); i++)
            {
                GameObject enemigo = tiposDeEnemigos[UnityEngine.Random.Range(0, tiposDeEnemigos.Length)];
                Vector3 posicionDespliege = new Vector3(CalcularCoordenadaX(enemigo), zonaDespliegeEnemigos.y, zonaDespliegeEnemigos.z);
                Quaternion rotacionDespliege = Quaternion.identity;
                Instantiate(enemigo, posicionDespliege, rotacionDespliege);
                yield return new WaitForSeconds(pausaEntreEnemigos);
            }

            //Instanciamos un powerUp aleatoriamente
            GameObject powerUp = tiposDePowerUps[UnityEngine.Random.Range(0, tiposDePowerUps.Length)];
            Vector3 posicion = new Vector3(UnityEngine.Random.Range(-zonaDespliegeEnemigos.x, zonaDespliegeEnemigos.x), zonaDespliegeEnemigos.y, zonaDespliegeEnemigos.z);
            Quaternion rotacion = Quaternion.identity;
            Instantiate(powerUp, posicion, rotacion);


            yield return new WaitForSeconds(pausaEntreOleadas);

            if (gameOver)
            {
                restart = true;
                break;
            }
        }
    }

    private float CalcularCoordenadaX(GameObject enemigo)
    {
        //Las formaciones son mayores por lo que hay que reducir la zona de despliege
        Formacion formacion = enemigo.GetComponent<Formacion>();
        if (formacion != null) //Es una formacion
        {
            return UnityEngine.Random.Range(-zonaDespliegeEnemigos.x + formacion.CorreccionEjeX, zonaDespliegeEnemigos.x - formacion.CorreccionEjeX);
        }
        else
        {
            return UnityEngine.Random.Range(-zonaDespliegeEnemigos.x, zonaDespliegeEnemigos.x);
        }
    }

    private int CalcularNumeroEnemigos()
    {
        int enemigosOleada = numeroEnemigos + levelManager.GetNivelActual().Enemigos;
        //Debug.Log("Enemigos por oleada: " + enemigosOleada);
        return enemigosOleada;
    }

    #region INotificador

    /// <summary>
    /// Realiza una notificación al usuario (duración por defecto)
    /// </summary>
    /// <param name="texto">Texto de la notificación</param>
    public void Notificar(string texto)
    {
        StartCoroutine(RealizarNotificacion(texto, CONFIGURACION.TIEMPO_NOTIFICACIONES));
    }

    /// <summary>
    /// Realiza una notificación al usuario
    /// </summary>
    /// <param name="texto">Texto de la notificación</param>
    /// <param name="duracion">Duración de la notificación</param>
    public void Notificar(string texto, float duracion)
    {
        StartCoroutine(RealizarNotificacion(texto, duracion));
    }

    /// <summary>
    /// Realiza una notificación temporizada
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="duracion"></param>
    /// <returns></returns>
    IEnumerator RealizarNotificacion(string texto, float duracion)
    {
        if(textoNotificaciones != null) //Establezco el texto de la notificación
        {
            textoNotificaciones.SetText(texto);
        }

        yield return new WaitForSeconds(duracion);

        if (textoNotificaciones != null) //Vacío la notificación
        {
            textoNotificaciones.SetText("");
        }
    }

    #endregion INotificador
}

public interface INotificador
{
    void Notificar(string texto);
    void Notificar(string texto, float duracion);
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        StartCoroutine(SpawnWaves());
    }

    public void GameOver()
    {
        textoEstado.SetText("Game Over!");
        gameOver = true;
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

    /// <summary>
    /// Realiza una notificación al usuario
    /// </summary>
    /// <param name="texto"></param>
    public void Notificar(string texto)
    {
        StartCoroutine(RealizarNotificacion(texto));
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
            if(!gameOver) textoEstado.SetText("");
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(pausaInicial);
            for (int i = 0; i < calcularNumeroEnemigos(); i++)
            {
                GameObject enemigo = tiposDeEnemigos[UnityEngine.Random.Range(0, tiposDeEnemigos.Length)];
                Vector3 posicionDespliege = new Vector3(UnityEngine.Random.Range(-zonaDespliegeEnemigos.x, zonaDespliegeEnemigos.x), zonaDespliegeEnemigos.y, zonaDespliegeEnemigos.z);
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

    private int calcularNumeroEnemigos()
    {
        int enemigosOleada = numeroEnemigos + levelManager.GetNivelActual().Enemigos;
        Debug.Log("Enemigos por oleada: " + enemigosOleada);
        return enemigosOleada;
    }

    IEnumerator RealizarNotificacion(string texto)
    {
        if(textoNotificaciones != null) //Establezco el texto de la notificación
        {
            textoNotificaciones.SetText(texto);
        }

        yield return new WaitForSeconds(CONFIGURACION.TIEMPO_NOTIFICACIONES);

        if (textoNotificaciones != null) //Vacío la notificación
        {
            textoNotificaciones.SetText("");
        }
    }
}

public interface INotificador
{
    void Notificar(string texto);
}

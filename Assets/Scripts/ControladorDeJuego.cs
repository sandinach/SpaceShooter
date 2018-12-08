﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControladorDeJuego : MonoBehaviour
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
    public GameObject MenuOpciones;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool pausa;
    private int numeroEnemigos;

    private ControladorDeDificultad levelManager = new ControladorDeDificultad();

    void Start()
    {
        pausa = false;
        score = 0;
        restart = false;
        gameOver = false;

        //Valor inicial para el número de enemigos
        numeroEnemigos = (int) CONFIGURACION.GetEnemigosPorOleada();

        textoEstado.SetText("");
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
            UpdateScore();
    }

    /// <summary>
    /// Obtiene el nivel de juego actual (se usa durante el proceso de configuración de enemigos)
    /// </summary>
    /// <returns></returns>
    public NivelDificultad GetNivelDificultad()
    {
        return levelManager.GetCurrentLevel();
    }

    private void UpdateScore()
    {
        textoPuntuacion.SetText("Puntos: " + score);
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
            for (int i = 0; i < numeroEnemigos; i++)
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
}

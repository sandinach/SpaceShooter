using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para centralizar la gestión de parámetros de los enemigos
/// </summary>
public class ControladorDeEnemigos : MonoBehaviour
{
    public Movimiento movimiento;
    public SistemaArmamento sistemaArmamento;
    public MovimientoEvasivo movimientoEvasivo;

    private ControladorDeJuego controladorDeJuego;
    private NivelDificultad nivelDificultad;

	// Use this for initialization
	void Start ()
    {
        //Al instanciar debo recuperar el nivel actual para modificar los valores
        GameObject gameControlerObject = GameObject.FindWithTag("GameController");
        if (gameControlerObject != null)
        {
            controladorDeJuego = gameControlerObject.GetComponent<ControladorDeJuego>();
        }
        if (controladorDeJuego == null)
        {
            Debug.Log("No se ha encontrado 'ControladorDeJuego' script");
        }
        else
        {
            ajustarDificultad();
        }
    }

    /// <summary>
    /// Ajusta la dificultad del enemigo en base a los parámetros establecidos en el nivel
    /// </summary>
    private void ajustarDificultad()
    {
        nivelDificultad = controladorDeJuego.GetNivelDificultad();

        //Establezco los nuevos valores y actualizo
        if (movimiento != null)
        {
            movimiento.Actualizar(nivelDificultad.Velocidad);
        }

        //Establezco los nuevos valores y actualizo
        if (sistemaArmamento != null)
        {
            sistemaArmamento.CadenciaDeDisparo = nivelDificultad.Cadencia;
            sistemaArmamento.Actualizar();
        }

        if (movimientoEvasivo != null)
        {
            movimientoEvasivo.enabled = nivelDificultad.UsarMovimientoEvasivo;
        }
    }
}

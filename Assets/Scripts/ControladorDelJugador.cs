﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimiteArea
{
    public float xMin, xMax, zMin, zMax;
}

public class ControladorDelJugador : MonoBehaviour
{
    public float velocidad;
    public float inclinacion;
    public LimiteArea limites;

    /// <summary>
    /// Munición
    /// </summary>
    public GameObject Municion;
    /// <summary>
    /// Ranuras de armamento
    /// </summary>
    public Transform[] RanurasDisparo;
    /// <summary>
    /// Velocidad de disparo (cadencia)
    /// </summary>
    public float CadenciaDeDisparo;

    private float siguienteDisparo;

    public ControladorDelJugador()
    {

    }

    private void Update()
    {
        //Controlamos los disparos en base a la cadencia de tiro
        if (Input.GetButton("Fire1") && Time.time > siguienteDisparo)
        {
            siguienteDisparo = Time.time + CadenciaDeDisparo;
            foreach (var ranura in RanurasDisparo)
            {
                EjecutarDisparo(Municion, ranura);
            }
        }
    }

    private void EjecutarDisparo(GameObject toTipoDisparo, Transform toPosicion)
    {
        Instantiate(toTipoDisparo, toPosicion.position, toPosicion.rotation);
    }

    private void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        GetComponent<Rigidbody>().velocity = movement * velocidad;

        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, limites.xMin, limites.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, limites.zMin, limites.zMax)
            );

        //Para dar efecto de inclinación
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * - inclinacion);
    }
}

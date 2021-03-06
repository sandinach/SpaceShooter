﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaArmamento : MonoBehaviour {

    public float CadenciaDeDisparo;
    public float EsperaInicial;
    public GameObject Municion;
    public Transform[] shotSpawns;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Disparar", EsperaInicial, CadenciaDeDisparo);
    }

    public void Actualizar()
    {
        CancelInvoke("Disparar"); //Cancelo la secuencia de repetición actual
        InvokeRepeating("Disparar", EsperaInicial, CadenciaDeDisparo); //Invoco con los nuevos parámetros
    }

    void Disparar()
    {
        if (Municion != null)
        {
            foreach (var shotSpawn in shotSpawns)
            {
                EjecutarDisparo(Municion, shotSpawn);
            }
        }

    }

    private void EjecutarDisparo(GameObject toTipoDisparo, Transform toPosicion)
    {
        Instantiate(toTipoDisparo, toPosicion.position, toPosicion.rotation);
    }
}

using System;
using System.Collections;
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
    /// Munición
    /// </summary>
    public GameObject MunicionSecundaria;
    /// <summary>
    /// Ranuras de armamento
    /// </summary>
    public Transform[] RanurasDisparo;
    /// <summary>
    /// Ranuras de armamento
    /// </summary>
    public Transform[] RanurasDisparoSecundario;
    /// <summary>
    /// Velocidad de disparo (cadencia)
    /// </summary>
    public float CadenciaDeDisparo;
    /// <summary>
    /// Velocidad de disparo (cadencia)
    /// </summary>
    public float CadenciaDeDisparoSecundario;

    private float siguienteDisparo;
    private float siguienteDisparoSecundario;
    private ControladorDeEnergia controladorDeEnergia;

    public ControladorDelJugador()
    {

    }

    private void Start()
    {
        GameObject controlEnergiaObject = GameObject.FindWithTag("ControlDeEnergia");
        if (controlEnergiaObject != null)
        {
            controladorDeEnergia = controlEnergiaObject.GetComponent<ControladorDeEnergia>();
        }
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
        else
        {
            if (Input.GetButton("Fire2") && Time.time > siguienteDisparoSecundario && tengoEnergia())
            {
                siguienteDisparoSecundario = Time.time + CadenciaDeDisparoSecundario;
                foreach (var ranura in RanurasDisparoSecundario)
                {
                    EjecutarDisparo(MunicionSecundaria, ranura);
                }
            }
        }
    }

    private bool tengoEnergia()
    {
        if(controladorDeEnergia != null && controladorDeEnergia.ObtenerEnergia(5))
        {
            return true;
        }
        return false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    /// <summary>
    /// Velocidad de desplazamiento
    /// </summary>
    public float velocidad;

    public void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * velocidad;
    }
}

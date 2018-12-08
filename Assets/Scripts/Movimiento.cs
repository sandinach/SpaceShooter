using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    /// <summary>
    /// Velocidad inicial de desplazamiento
    /// </summary>
    public float velocidad;

    public void Start()
    {
        SetValue(velocidad); //Establece el valor inicial
    }

    /// <summary>
    /// Actualitza el valor
    /// </summary>
    public void Actualizar()
    {
        SetValue(velocidad);
    }

    /// <summary>
    /// Establece el valor
    /// </summary>
    private void SetValue(float valor)
    {
        GetComponent<Rigidbody>().velocity = transform.forward * valor;
    }
}

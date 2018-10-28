using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aplica una rotación aleatoria a la velocidad angular
/// </summary>
public class RotacionAleatoria : MonoBehaviour
{
    /// <summary>
    /// Valor constante
    /// </summary>
    private const float tumble = 5.0f;

    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}

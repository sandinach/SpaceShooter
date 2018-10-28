using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para destruir un objeto de forma temporizada, se utiliza por ejemplo en la explosiones
/// </summary>
public class DestruirPorTiempo : MonoBehaviour
{ 
    /// <summary>
    /// Determina el tiempo de vida del objeto
    /// </summary>
    public float tiempoDeVida;

    /// <summary>
    /// Al iniciar...
    /// </summary>
	void Start ()
    {
        Destroy(gameObject, tiempoDeVida);
	}

}

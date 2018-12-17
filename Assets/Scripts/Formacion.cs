using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Clase ficticia para determinar si se trata de una formación y poder realizar ajustes
/// </summary>
public class Formacion : MonoBehaviour
{
    public List<GameObject> naves;

    /// <summary>
    /// Corrección de la posición de instanciamiento
    /// </summary>
    public float CorreccionEjeX = 2.5f;
    public float CorreccionInstanciamiento = 0.4f;

    private void Update()
    {
        //Miro si ya no queda nada de la formación
        if (naves.Where(nave => nave.Equals(null)).Count() == 3)
        {
            //Debug.Log("No quedan naves");
            Destroy(this.gameObject); //Destruyo la formación
        }
    }
}

using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSincronizado : Movimiento
{
    public GameObject[] naves;

    /// <summary>
    /// Establece el valor
    /// </summary>
    protected override void SetValue(float valor)
    {
        foreach (var nave in naves)
        {
            if (nave != null)
            {
                nave.GetComponent<Rigidbody>().velocity = transform.forward * valor;
            }
        }
        GestionarDestruidos();
    }

    private void GestionarDestruidos()
    {
        if (naves.All(nave => nave == null))
            Destroy(this);
    }
}


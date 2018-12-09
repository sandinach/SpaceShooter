using System.Linq;
using UnityEngine;

public class MovimientoEvasivoSincronizado : MovimientoEvasivo
{
    public GameObject[] naves;

    protected override void Inicializar()
    {
        rb = naves[0].GetComponent<Rigidbody>();
        velocidadActual = rb.velocity.z;
    }

    protected override void Actualizar()
    {
        foreach (var nave in naves)
        {
            if (nave != null)
            {
                ActualizarRigitBody(nave.GetComponent<Rigidbody>());
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

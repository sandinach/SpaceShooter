using UnityEngine;

public class Movimiento : MonoBehaviour, IMovimiento
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
    public void Actualizar(float nuevaVelocidad)
    {
        velocidad = nuevaVelocidad;
        SetValue(nuevaVelocidad);
    }

    /// <summary>
    /// Establece el valor
    /// </summary>
    protected virtual void SetValue(float valor)
    {
        GetComponent<Rigidbody>().velocity = transform.forward * valor;
    }
}

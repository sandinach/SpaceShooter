using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEvasivo : MonoBehaviour {

    public float destino; //X maxima
    public float smoothing; //para suavizar el movimiento
    public float inclinacion;
    public Vector2 esperaInicial;
    public Vector2 duracionManiobra;
    public Vector2 esperaManiobra;
    public LimiteArea limite;

    private float destinoManiobra;
    private float velocidadActual;
    private Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadActual = rb.velocity.z;

        StartCoroutine(Evadir());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newManuever = Mathf.MoveTowards(rb.velocity.x, destinoManiobra, Time.deltaTime + smoothing);
        rb.velocity = new Vector3(newManuever, 0.0f, velocidadActual);
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, limite.xMin, limite.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, limite.zMin, limite.zMax)
            );
        //inclinación
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -inclinacion);
    }

    IEnumerator Evadir()
    {
        yield return new WaitForSeconds(Random.Range(esperaInicial.x, esperaInicial.y));

        while (true)
        {
            destinoManiobra = Random.Range(1, destino) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(duracionManiobra.x, duracionManiobra.y));
            destinoManiobra = 0;
            yield return new WaitForSeconds(Random.Range(esperaManiobra.x, esperaManiobra.y));
        }
    }
}

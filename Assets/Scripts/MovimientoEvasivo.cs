﻿using System.Collections;
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
    protected float velocidadActual;
    protected Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        Inicializar();

        StartCoroutine(Evadir());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Actualizar();
    }

    protected virtual void Inicializar()
    {
        rb = GetComponent<Rigidbody>();
        velocidadActual = rb.velocity.z;
    }

    protected virtual void Actualizar()
    {
        
        ActualizarRigitBody(rb);
    }

    protected void ActualizarRigitBody(Rigidbody rigidbody)
    {
        velocidadActual = rigidbody.velocity.z;
        float newManuever = Mathf.MoveTowards(rigidbody.velocity.x, destinoManiobra, Time.deltaTime + smoothing);
        rigidbody.velocity = new Vector3(newManuever, 0.0f, velocidadActual);
        rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, limite.xMin, limite.xMax),
                0.0f,
                Mathf.Clamp(rigidbody.position.z, limite.zMin, limite.zMax)
            );
        //inclinación
        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -inclinacion);
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

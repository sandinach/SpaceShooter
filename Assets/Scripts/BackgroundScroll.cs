using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public float velocidadScroll;
    public float medidaLienzo;

    private Vector3 posicionInicial;

    // Use this for initialization
    void Start()
    {
        posicionInicial = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float newPostition = Mathf.Repeat(Time.time * velocidadScroll, medidaLienzo);
        transform.position = posicionInicial + Vector3.forward * newPostition;
    }
}

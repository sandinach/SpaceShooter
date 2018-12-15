using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorContacto : MonoBehaviour
{
    public int puntuacion;
    public int daño = 25;
    private ControladorDeJuego controladorDeJuego;
    private ControladorDeVida controladorDeVida;

    public GameObject explosionPropia;
    /// <summary>
    /// Por si se quiere usar algún tipo de animación especial de impacto
    /// </summary>
    public GameObject explosionAjena;

    private void Start()
    {
        GameObject gameControlerObject = GameObject.FindWithTag("GameController");
        if (gameControlerObject != null)
        {
            controladorDeJuego = gameControlerObject.GetComponent<ControladorDeJuego>();
        }
        if (controladorDeJuego == null)
        {
            Debug.Log("No se ha encontrado 'ControladorDeJuego' script");
        }

        GameObject controlObject = GameObject.FindWithTag("ControlDeVida");
        if (controlObject != null)
        {
            controladorDeVida = controlObject.GetComponent<ControladorDeVida>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AreaDeJuego") || other.CompareTag("Enemigo"))
        {
            return;
        }

        Explotar();

        bool destruir = true;
        if (other.CompareTag("Player"))
        {
            //El jugador define su propia animación de explosión
            AnimacionDeDestruccion animacion = other.gameObject.GetComponent<AnimacionDeDestruccion>();
            destruir = controladorDeVida.Impacto(daño);
            if(destruir && animacion != null)
                animacion.Destruir(other.transform);
        }
        else
        {
            if(explosionAjena != null)
                Instantiate(explosionAjena, other.transform.position, other.transform.rotation);
        }

        controladorDeJuego.AddScore(puntuacion);
        if(destruir) //Para objetos normales es true, para el jugador depende de la vida
            Destroy(other.gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// Método para iniciar la explosión del objeto
    /// </summary>
    public void Explotar()
    {
        if (explosionPropia != null)
        {
            Instantiate(explosionPropia, transform.position, transform.rotation);
        }
    }
}

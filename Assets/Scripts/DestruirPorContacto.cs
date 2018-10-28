using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorContacto : MonoBehaviour
{
    public int puntuacion;
    private ControladorDeJuego controladorDeJuego;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AreaDeJuego") || other.CompareTag("Enemigo"))
        {
            return;
        }

        Explotar();

        if (other.CompareTag("Player"))
        {
            //El jugador define su propia animación de explosión
            AnimacionDeDestruccion animacion = other.gameObject.GetComponent<AnimacionDeDestruccion>();
            if(animacion != null)
                animacion.Destruir(other.transform);
            //Instantiate(explosionJugador, other.transform.position, other.transform.rotation);
            controladorDeJuego.GameOver();
        }
        else
        {
            if(explosionAjena != null)
                Instantiate(explosionAjena, other.transform.position, other.transform.rotation);
        }

        controladorDeJuego.AddScore(puntuacion);
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

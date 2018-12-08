using UnityEngine;

public class PowerUpVida : MonoBehaviour {

    /// <summary>
    /// Animación de recogida del powerUp
    /// </summary>
    public GameObject animacion;

    /// <summary>
    /// Valor del PowerUP
    /// </summary>
    public int valor = 20;

    private ControladorDeVida controladorDeVida;

    private void Start()
    {
        GameObject controlObject = GameObject.FindWithTag("ControlDeVida");
        if (controlObject != null)
        {
            controladorDeVida = controlObject.GetComponent<ControladorDeVida>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            recogerPorwerUp();
        }
    }

    private void recogerPorwerUp()
    {
        if (animacion != null) //Si tengo animación la disparo
            Instantiate(animacion, transform.position, transform.rotation);

        if (controladorDeVida != null)
        {
            controladorDeVida.AumentarVida(valor);
            Debug.Log("Salud del jugador " + controladorDeVida.VidaActual());
        }

        Destroy(gameObject); //Destruyo el power Up
    }
}

using UnityEngine;

public class PowerUpEnergia : MonoBehaviour {

    /// <summary>
    /// Animación de recogida del powerUp
    /// </summary>
    public GameObject animacion;

    /// <summary>
    /// Valor del PowerUP
    /// </summary>
    public int valor =20;

    private ControladorDeEnergia controladorDeEnergia;

    private void Start()
    {
        GameObject controlObject = GameObject.FindWithTag("ControlDeEnergia");
        if (controlObject != null)
        {
            controladorDeEnergia = controlObject.GetComponent<ControladorDeEnergia>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            recogerPorwerUp();
        }
    }

    private void recogerPorwerUp()
    {
        if (animacion != null) //Si tengo animación la disparo
            Instantiate(animacion, transform.position, transform.rotation);

        if(controladorDeEnergia != null)
        {
            controladorDeEnergia.AumentarEnergia(valor);
        }

        Destroy(gameObject); //Destruyo el power Up
    }
}

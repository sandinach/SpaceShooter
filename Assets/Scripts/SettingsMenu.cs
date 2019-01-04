using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderEfectos;
    public Slider sliderMusica;
    public Slider sliderNumeroEnemigos;
    public Slider sliderGeneracionEnergia;
    public Slider sliderEnergiaInicial;

    private void Start()
    {
        //Debug.Log("SettingsMenu.Start");
        InicializarValores();
    }

    private void InicializarValores()
    {
        float volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 0.0f);
        float volumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.0f);
        sliderEfectos.value = volumenEfectos;
        sliderMusica.value = volumenMusica;

        //Configuro el control (Máximo, mínimo y valor por defecto)
        configurarSlider(sliderNumeroEnemigos, CONFIGURACION.GetConfigEnemigosPorOleada());
        configurarSlider(sliderGeneracionEnergia, CONFIGURACION.GetConfigGeneracionEnergia());
        configurarSlider(sliderEnergiaInicial, CONFIGURACION.GetConfigEnergiaInicial());

        //Recupero los valores
        sliderNumeroEnemigos.value = CONFIGURACION.GetEnemigosPorOleada();
        sliderGeneracionEnergia.value = CONFIGURACION.GetGeneracionEnergia();
        sliderEnergiaInicial.value = CONFIGURACION.GetEnergiaInicial();
}

    private void configurarSlider(Slider control, SliderConfig config)
    {
        control.minValue = config.GetMin();
        control.value = config.GetCurrent();
        control.maxValue = config.GetMax();
    }

    public void SetVolumenMusica(float volume)
    {
        audioMixer.SetFloat("VolumenMusica", volume);
        PlayerPrefs.SetFloat("VolumenMusica", volume); //Que vaya guardando los valores
    }

    public void SetVolumenEfectos(float volume)
    {
        audioMixer.SetFloat("VolumenEfectos", volume);
        PlayerPrefs.SetFloat("VolumenEfectos", volume); //Que vaya guardando los valores
    }

    public void SetNumeroEnemigos(float valor)
    {
        CONFIGURACION.SetEnemigosPorOleada(valor);
    }

    public void SetEnergiaInicial(float valor)
    {
        CONFIGURACION.SetEnergiaInicial(valor);
    }

    public void SetGeneracionEnergia(float valor)
    {
        CONFIGURACION.SetGeneracionEnergia(valor);
    }

    public void RestablecerPreferencias()
    {
        PlayerPrefs.DeleteAll();

        //Reseteo las clases con los valores iniciales
        CONFIGURACION.GetConfigEnemigosPorOleada().Reset();
        CONFIGURACION.GetConfigGeneracionEnergia().Reset();
        CONFIGURACION.GetConfigEnergiaInicial().Reset();

        //Cargo los valores iniciales y ya no se requiere reinicio
        InicializarValores();
    }
}

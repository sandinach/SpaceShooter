using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderEfectos;
    public Slider sliderMusica;
    public Slider sliderNumeroEnemigos;
    public Slider sliderVelocidad;

    private void Start()
    {
        float volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 0.0f);
        float volumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.0f);
        sliderEfectos.value = volumenEfectos;
        sliderMusica.value = volumenMusica;

        configurarSlider(sliderNumeroEnemigos, CONFIGURACION.GetConfigEnemigosPorOleada());
        configurarSlider(sliderVelocidad, CONFIGURACION.GetConfigVelocidadEnemigos());
    }

    private void configurarSlider(Slider control, SliderConfigInt config)
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
        sliderNumeroEnemigos.value = valor;
        CONFIGURACION.SetEnemigosPorOleada(Convert.ToInt32(Math.Round(valor, 0)));
    }

    public void SetVelocidadEnemigos(float valor)
    {
        sliderVelocidad.value = valor;
        CONFIGURACION.SetEnemigosPorOleada(Convert.ToInt32(Math.Round(valor,0)));
    }
}

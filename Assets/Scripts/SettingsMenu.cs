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
    public Slider sliderGeneracionEnergia;
    public Slider sliderEnergiaInicial;

    private void Start()
    {
        float volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 0.0f);
        float volumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.0f);
        sliderEfectos.value = volumenEfectos;
        sliderMusica.value = volumenMusica;

        configurarSlider(sliderNumeroEnemigos, CONFIGURACION.GetConfigEnemigosPorOleada());
        configurarSlider(sliderVelocidad, CONFIGURACION.GetConfigVelocidadEnemigos());
        configurarSlider(sliderGeneracionEnergia, CONFIGURACION.GetConfigGeneracionEnergia());
        configurarSlider(sliderEnergiaInicial, CONFIGURACION.GetConfigEnergiaInicial());
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

    public void SetVelocidadEnemigos(float valor)
    {
        CONFIGURACION.SetVelocidadEnemigos(valor);
    }

    public void SetEnergiaInicial(float valor)
    {
        CONFIGURACION.SetEnergiaInicial(valor);
    }

    public void SetGeneracionEnergia(float valor)
    {
        CONFIGURACION.SetGeneracionEnergia(valor);
    }
}

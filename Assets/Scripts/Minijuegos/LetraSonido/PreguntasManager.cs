using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class PreguntasManager : MonoBehaviour
{
    public Text preguntaText;
    public Image preguntaImagen;
    public Text vidasText;
    public Text erroresContador;
    public Text rondasContador;
    public Button[] respuestaBotones;
    public PreguntasData preguntasData;
    // Audio de la pregunta
    public Button botonPlayPregunta;
    private AudioSource audioPregunta;

    // Game Objects para mostrar los diferentes paneles
    public GameObject Correcto;
    public GameObject Incorrecto;
    public GameObject Estadisticas;

    private int preguntaActual = 0;
    private static int vidas = 3;
    private static int rondas = 0;

    private void Start()
    {
        // Asigna el componente AudioSource
        audioPregunta = gameObject.AddComponent<AudioSource>();

        SetPregunta(preguntaActual);

        // Ocultar los paneles de error/acierto/estadísticas
        Correcto.gameObject.SetActive(false);
        Incorrecto.gameObject.SetActive(false);
        Estadisticas.gameObject.SetActive(false);

        // Añade el listener para el botón de reproducir sonido
        botonPlayPregunta.onClick.AddListener(ReproducirSonido);
    }

    void SetPregunta(int preguntaIndex)
    {
        // Seteamos los textos de la interfaz (pregunta y rondas)
        preguntaText.text = preguntasData.preguntas[preguntaIndex].preguntaText;
        rondasContador.text = "" + rondas;

        // Seteamos la imagen de la pregunta
        preguntaImagen.sprite = preguntasData.preguntas[preguntaIndex].imagen;

        // Asigna el clip de audio de la pregunta actual
        audioPregunta.clip = preguntasData.preguntas[preguntaIndex].audio;

        // Quitar Listeners anteriores antes de agregar nuevos
        foreach (Button r in respuestaBotones)
        {
            r.onClick.RemoveAllListeners();
        }

        // Añadimos listeners y revisamos si la respuesta es correcta cuando dé clic en un botón
        for (int i = 0; i < respuestaBotones.Length; i++)
        {
            respuestaBotones[i].GetComponentInChildren<Text>().text = preguntasData.preguntas[preguntaIndex].respuestas[i];
            int respuestaIndex = i;
            respuestaBotones[i].onClick.AddListener(() =>
            {
                CheckRespuesta(respuestaIndex);
            });

        }
    }

    void ReproducirSonido()
    {
        // Reproduce el sonido de la pregunta actual
        audioPregunta.Play();
    }

    void CheckRespuesta(int respuestaIndex)
    {
        // Si la respuesta es CORRECTA
        if (respuestaIndex == preguntasData.preguntas[preguntaActual].indiceRespuestaCorrecta)
        {
            // Mostramos el panel de respuesta correcta
            Correcto.gameObject.SetActive(true);

            // Desactivamos los botones de respuesta
            foreach(Button r in respuestaBotones)
            {
                r.interactable = false;
            }

            // Siguiente pregunta
            StartCoroutine(Next());
        }
        // Si la respuesta es INCORRECTA
        else
        {
            // Disminuimos las vidas
            vidas--;
            vidasText.text = "" + vidas;

            // Mostramos el panel de respuesta incorrecta
            Incorrecto.gameObject.SetActive(true);

            // Desactivamos los botones de respuesta
            foreach (Button r in respuestaBotones)
            {
                r.interactable = false;
            }

            StartCoroutine(RespuestaIncorrecta());
        }
    }

    IEnumerator RespuestaIncorrecta()
    {
        yield return new WaitForSeconds(2);

        Incorrecto.SetActive(false);

        // Habilitamos los botones de respuesta
        foreach (Button r in respuestaBotones)
        {
            r.interactable = true;
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2);

        preguntaActual++;
        
        // Si siguen existiendo preguntas se hace un Reset
        if (preguntaActual < preguntasData.preguntas.Length)
        {
            rondas++;
            Reset();
        }
        // Si no, se muestra la pantalla de Estadísticas
        else
        {
            Estadisticas.SetActive(true);

            erroresContador.text = vidas - 3 + "";

        }

    }

    public void Reset()
    {
        // Se esconden los paneles de correcto/incorrecto
        Correcto.SetActive(false);
        Incorrecto.SetActive(false);

        // Habilitamos los botones de respuesta
        foreach (Button r in respuestaBotones)
        {
            r.interactable = true;
        }

        // Setteamos la siguiente pregunta
        SetPregunta(preguntaActual);
    }
}

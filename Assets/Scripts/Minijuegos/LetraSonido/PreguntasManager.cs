using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreguntasManager : MonoBehaviour
{
    public Text preguntaText;
    public Image preguntaImagen;
    public Text vidasText;
    public Text erroresContador;
    public Text rondasContador;
    public Text tiempoText;

    // Botones para las respuestas
    public Button[] respuestaBotones;

    // Archivo con el set de preguntas-respuestas
    public PreguntasData preguntasData;

    // Audio de la pregunta
    public Button botonPlayPregunta;
    private AudioSource audioPregunta;

    // Audio de la respuesta
    private AudioSource audioRespuesta;
    public Button[] playRespuestaBotones;

    // Game Objects para mostrar los diferentes paneles
    public GameObject Correcto;
    public GameObject Incorrecto;
    public GameObject Estadisticas;
    public GameObject PartidaTerminada;

    // Variables internas para llevar la cuenta de la pregunta actual, vidas y rondas
    private int preguntaActual = 0;
    private static int vidas = 3;
    private static int rondas = 0;

    // Variables para el contador de tiempo
    private float tiempoNivel;
    private bool nivelActivo = false;

    private void Start()
    {
        // Asigna los componentes AudioSource
        audioPregunta = gameObject.AddComponent<AudioSource>();
        audioRespuesta = gameObject.AddComponent<AudioSource>();

        SetPregunta(preguntaActual);

        // Ocultar los paneles de error/acierto/estadísticas
        Correcto.gameObject.SetActive(false);
        Incorrecto.gameObject.SetActive(false);
        Estadisticas.gameObject.SetActive(false);
        PartidaTerminada.gameObject.SetActive(false);

        // Añade el listener para el botón de reproducir sonido
        botonPlayPregunta.onClick.AddListener(ReproducirSonidoPregunta);

        // Inicia el contador de tiempo
        tiempoNivel = 0f;
        nivelActivo = true;
    }

    private void Update()
    {
        if (nivelActivo)
        {
            tiempoNivel += Time.deltaTime;
            Debug.Log(tiempoNivel);
        }
    }

    void SetPregunta(int preguntaIndex)
    {
        // Seteamos los textos de la interfaz (pregunta y rondas)
        preguntaText.text = preguntasData.preguntas[preguntaIndex].preguntaText;
        rondasContador.text = "" + rondas;

        // Seteamos la imagen de la pregunta
        preguntaImagen.sprite = preguntasData.preguntas[preguntaIndex].imagen;

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

            // Añadir listener para reproducir el audio de la respuesta
            playRespuestaBotones[i].onClick.RemoveAllListeners();
            playRespuestaBotones[i].onClick.AddListener(() =>
            {
                ReproducirSonidoRespuesta(respuestaIndex);
            });
        }
    }

    void ReproducirSonidoPregunta()
    {
        // Reproduce el sonido de la pregunta actual
        // Asigna el clip de audio de la pregunta actual
        audioPregunta.clip = preguntasData.preguntas[preguntaActual].audio;
        audioPregunta.Play();
    }

    void ReproducirSonidoRespuesta(int respuestaIndex)
    {
        // Reproduce el sonido de la respuesta seleccionada
        audioRespuesta.clip = preguntasData.preguntas[preguntaActual].respuestasAudios[respuestaIndex];
        audioRespuesta.Play();
    }

    void CheckRespuesta(int respuestaIndex)
    {
        // Si la respuesta es CORRECTA
        if (respuestaIndex == preguntasData.preguntas[preguntaActual].indiceRespuestaCorrecta)
        {
            // Mostramos el panel de respuesta correcta
            Correcto.gameObject.SetActive(true);

            // Desactivamos los botones de respuesta
            DesactivarBotones();

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

            DesactivarBotones();

            StartCoroutine(RespuestaIncorrecta());
        }
    }

    void CheckVidas ()
    {
        if (vidas <= 0)
        {
            DesactivarBotones();
            PartidaTerminada.SetActive(true);
        }
    }

    IEnumerator RespuestaIncorrecta()
    {
        yield return new WaitForSeconds(2);

        Incorrecto.SetActive(false);

        // Habilitamos los botones de respuesta
        ActivarBotones();

        CheckVidas();
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

            erroresContador.text = Mathf.Abs(vidas - 3) + "";

            // Mostrar el tiempo tomado
            nivelActivo = false; // Detener el contador de tiempo
            tiempoText.text = (int)tiempoNivel + "";
        }
    }

    public void Reset()
    {
        // Se esconden los paneles de correcto/incorrecto
        Correcto.SetActive(false);
        Incorrecto.SetActive(false);

        // Habilitamos los botones de respuesta
        ActivarBotones();

        // Setteamos la siguiente pregunta
        SetPregunta(preguntaActual);
    }

    public void DesactivarBotones()
    {
        foreach (Button r in respuestaBotones)
        {
            r.interactable = false;
        }
    }

    public void ActivarBotones()
    {
        foreach (Button r in respuestaBotones)
        {
            r.interactable = true;
        }
    }
}

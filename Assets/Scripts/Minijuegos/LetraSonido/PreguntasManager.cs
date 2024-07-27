using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class PreguntasManager : MonoBehaviour
{
    public Text preguntaText;
    public Text vidasText;
    public Text erroresContador;
    public Button[] respuestaBotones;
    public PreguntasData preguntasData;

    // Game Objects para mostrar los diferentes paneles
    public GameObject Correcto;
    public GameObject Incorrecto;
    public GameObject Estadisticas;

    private int preguntaActual = 0;
    private static int vidas = 3;

    private void Start()
    {
        SetPregunta(preguntaActual);
        Correcto.gameObject.SetActive(false);
        Incorrecto.gameObject.SetActive(false);
        Estadisticas.gameObject.SetActive(false);
    }

    void SetPregunta(int preguntaIndex)
    {
        preguntaText.text = preguntasData.preguntas[preguntaIndex].preguntaText;

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

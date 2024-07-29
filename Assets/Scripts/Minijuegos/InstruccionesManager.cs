using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstruccionesManager : MonoBehaviour
{
    public GameObject panelInstruccionesUI;
    public GameObject[] instruccionesUI;
    public Button saltarBtn;

    private int numeroInstrucciones;

    void Start()
    {
        panelInstruccionesUI.SetActive(false);
        // Activamos el botón de saltar (por si se desactivó en una visualización anterior)
        //saltarBtn.gameObject.SetActive(true);

        numeroInstrucciones = instruccionesUI.Length;

        int contador = 0;
        // Añadimos listeners y se llama al método de siguiente
        foreach (GameObject panel in instruccionesUI)
        {
            if (panel != null)
            {
                Debug.Log(contador);
                // Buscamos en el panel los botones de siguiente, ok y audio
                Button btnSiguiente = panel.transform.Find("BtnSiguiente").GetComponent<Button>();
                Button btnReproduccionAudio = panel.transform.Find("BtnReproducirAudio").GetComponent<Button>();

                if (btnSiguiente != null)
                {
                    btnSiguiente.onClick.RemoveAllListeners();
                    btnSiguiente.onClick.AddListener(() => Siguiente(contador));
                    Debug.Log("encontró btn Siguiente");
                }

                if (btnReproduccionAudio != null)
                {
                    btnReproduccionAudio.onClick.RemoveAllListeners();
                    btnReproduccionAudio.onClick.AddListener(ReproducirAudio);
                    Debug.Log("encontro btn Reproduccion Audio");
                }
                Debug.Log("panel" + contador);
                contador++;
            }
        }

        // Agregamos el listener al botón de saltar
        saltarBtn.onClick.RemoveAllListeners();
        saltarBtn.onClick.AddListener(Saltar);
    }

    void Siguiente(int indiceRonda)
    {
        Debug.Log("ronda" + indiceRonda);
        // Evalúa: Si la instruccion actual + 1 es igual al índice de la última instrucción,
        // se oculta el botón de "Saltar" al pasar al siguiente Panel de Instrucción
        if (indiceRonda + 1 == numeroInstrucciones)
        {
            saltarBtn.gameObject.SetActive(false);
            instruccionesUI[indiceRonda].gameObject.SetActive(false);
            instruccionesUI[indiceRonda + 1].gameObject.SetActive(true);
        }
        // Si ya es la última instrucción se oculta todo el panel de instrucciones
        else if (indiceRonda == numeroInstrucciones)
        {
            Saltar();
        }
        // Si hay más instrucciones, se oculta el panel actual y se muestra el siguiente
        else
        {
            instruccionesUI[indiceRonda].gameObject.SetActive(false);
            instruccionesUI[indiceRonda+1].gameObject.SetActive(true);
        }

    }

    void Saltar()
    {
        panelInstruccionesUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    void ReproducirAudio()
    {
        Debug.Log("Clic reproducción de audio");
    }
}


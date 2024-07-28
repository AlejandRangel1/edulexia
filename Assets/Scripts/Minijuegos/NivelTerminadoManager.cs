using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NivelTerminadoManager : MonoBehaviour
{
    public Button btnReiniciarTerminado;
    public Button btnSalirTerminado;
    public Button btnReiniciarPerdiste;
    public Button btnSalirPerdiste;

    void Start()
    {
        btnReiniciarTerminado.onClick.AddListener(ReiniciarNivel);
        btnReiniciarPerdiste.onClick.AddListener(ReiniciarNivel);
        btnSalirTerminado.onClick.AddListener(Salir);
        btnSalirPerdiste.onClick.AddListener(Salir);
    }
    void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChooseGameScreen");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button btnJuegoLetraSonido;
    public GameObject popUp;

    // Start is called before the first frame update
    void Start()
    {
        // Asignar el método PasarANivel al evento onClick del botón
        btnJuegoLetraSonido.onClick.AddListener(() => PasarANivel("MinijuegoLetraSonido"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MostrarPopUp()
    {
        popUp.SetActive(true);
    }
    public void CerrarPopUp()
    {
        popUp.SetActive(false);
    }

    public void PasarANivel(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}

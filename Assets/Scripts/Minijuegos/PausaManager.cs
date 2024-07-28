using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausaManager : MonoBehaviour
{
    public GameObject menuPausaUI;
    public Button btnPausar;
    public Button btnReanudar;
    public Button btnReiniciar;
    public Button btnSalir;
    public Button btnCerrar;

    void Start()
    {
        menuPausaUI.SetActive(false);

        btnPausar.onClick.AddListener(Pausar);
        btnReanudar.onClick.AddListener(Reanudar);
        btnReiniciar.onClick.AddListener(ReiniciarNivel);
        btnSalir.onClick.AddListener(Salir);
        btnCerrar.onClick.AddListener(Reanudar);
    }

    void Reanudar()
    {
        // Se oculta el panel de pausa
        menuPausaUI.SetActive (false);
        CanvasGroup canvasGroup = menuPausaUI.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Se reanuda el tiempo
        Time.timeScale = 1f;
    }

    void Pausar()
    {
        // Se muestra el panel de pausa
        menuPausaUI.SetActive(true);
        CanvasGroup canvasGroup = menuPausaUI.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Se pausa el tiempo
        Time.timeScale = 0f;
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

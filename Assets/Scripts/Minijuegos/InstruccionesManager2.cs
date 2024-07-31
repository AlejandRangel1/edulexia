using UnityEngine;
using UnityEngine.UI;

public class InstruccionesManager2 : MonoBehaviour
{
    public GameObject canvasInstruccionesUI; // Canvas que contiene todas las instrucciones
    public Button btnSiguiente; // Bot�n para cambiar entre instrucciones
    public Button btnSaltar; // Bot�n para saltar las instrucciones
    public Button btnReproducirAudio; // Bot�n para reproducir audio de la instrucci�n actual
    public Sprite graficoFlecha; // Gr�fico de flecha para el bot�n
    public Sprite graficoOK; // Gr�fico de "OK" para el bot�n
    public GameObject[] panelesInstrucciones; // Array de paneles de instrucciones
    public AudioClip[] sonidosInstrucciones; // Array de clips de audio correspondientes a las instrucciones

    private int indiceActual = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        InicializarInstrucciones();
    }

    public void MostrarInstrucciones()
    {
        canvasInstruccionesUI.SetActive(true);
        InicializarInstrucciones();
    }

    private void InicializarInstrucciones()
    {
        indiceActual = 0;

        // Ocultar todos los paneles excepto el primero
        for (int i = 0; i < panelesInstrucciones.Length; i++)
        {
            panelesInstrucciones[i].SetActive(i == 0);
        }

        // Reiniciar el �ndice actual
        indiceActual = 0;

        // A�adir listener para el bot�n siguiente si no est�n ya a�adidos
        btnSiguiente.onClick.RemoveAllListeners();
        btnSiguiente.onClick.AddListener(CambiarInstruccion);

        // A�adir listener para el bot�n saltar si no est�n ya a�adidos
        btnSaltar.onClick.RemoveAllListeners();
        btnSaltar.onClick.AddListener(SaltarInstrucciones);

        // A�adir listener para el bot�n reproducir audio si no est�n ya a�adidos
        btnReproducirAudio.onClick.RemoveAllListeners();
        btnReproducirAudio.onClick.AddListener(ReproducirAudio);

        // Actualizar el bot�n seg�n la instrucci�n actual
        ActualizarBoton();

        // Desactivar el Raycast Target de todos los elementos UI en los paneles
        foreach (var panel in panelesInstrucciones)
        {
            DesactivarRaycastTargets(panel);
        }
    }

    private void CambiarInstruccion()
    {
        // Si estamos en el �ltimo panel, cerrar el canvas
        if (indiceActual >= panelesInstrucciones.Length - 1)
        {
            canvasInstruccionesUI.SetActive(false);
            return;
        }

        // Ocultar el panel actual
        panelesInstrucciones[indiceActual].SetActive(false);

        // Incrementar el �ndice actual
        indiceActual++;

        // Mostrar el siguiente panel
        panelesInstrucciones[indiceActual].SetActive(true);

        // Actualizar el bot�n seg�n la instrucci�n actual
        ActualizarBoton();
    }

    private void SaltarInstrucciones()
    {
        // Ocultar el canvas de instrucciones
        canvasInstruccionesUI.SetActive(false);
    }

    private void ActualizarBoton()
    {
        // Si es la �ltima instrucci�n
        if (indiceActual == panelesInstrucciones.Length - 1)
        {
            // Cambiar el gr�fico del bot�n a "OK"
            btnSiguiente.GetComponent<Image>().sprite = graficoOK;

            // Ocultar el bot�n de saltar
            btnSaltar.gameObject.SetActive(false);
        }
        else
        {
            // Cambiar el gr�fico del bot�n a flecha
            btnSiguiente.GetComponent<Image>().sprite = graficoFlecha;

            // Mostrar el bot�n de saltar
            btnSaltar.gameObject.SetActive(true);
        }
    }

    private void DesactivarRaycastTargets(GameObject panel)
    {
        // Obt�n todos los componentes UI que tengan la propiedad RaycastTarget
        Graphic[] uiComponents = panel.GetComponentsInChildren<Graphic>();

        // Desactiva el Raycast Target en cada componente
        foreach (var component in uiComponents)
        {
            component.raycastTarget = false;
        }
    }

    private void ReproducirAudio()
    {
        if (indiceActual < sonidosInstrucciones.Length)
        {
            audioSource.clip = sonidosInstrucciones[indiceActual];
            audioSource.Play();
        }
    }
}

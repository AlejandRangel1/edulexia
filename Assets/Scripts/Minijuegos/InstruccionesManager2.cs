using UnityEngine;
using UnityEngine.UI;

public class InstruccionesManager2 : MonoBehaviour
{
    public GameObject canvasInstruccionesUI; // Canvas que contiene todas las instrucciones
    public Button btnSiguiente; // Botón para cambiar entre instrucciones
    public Button btnSaltar; // Botón para saltar las instrucciones
    public Button btnReproducirAudio; // Botón para reproducir audio de la instrucción actual
    public Sprite graficoFlecha; // Gráfico de flecha para el botón
    public Sprite graficoOK; // Gráfico de "OK" para el botón
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

        // Reiniciar el índice actual
        indiceActual = 0;

        // Añadir listener para el botón siguiente si no están ya añadidos
        btnSiguiente.onClick.RemoveAllListeners();
        btnSiguiente.onClick.AddListener(CambiarInstruccion);

        // Añadir listener para el botón saltar si no están ya añadidos
        btnSaltar.onClick.RemoveAllListeners();
        btnSaltar.onClick.AddListener(SaltarInstrucciones);

        // Añadir listener para el botón reproducir audio si no están ya añadidos
        btnReproducirAudio.onClick.RemoveAllListeners();
        btnReproducirAudio.onClick.AddListener(ReproducirAudio);

        // Actualizar el botón según la instrucción actual
        ActualizarBoton();

        // Desactivar el Raycast Target de todos los elementos UI en los paneles
        foreach (var panel in panelesInstrucciones)
        {
            DesactivarRaycastTargets(panel);
        }
    }

    private void CambiarInstruccion()
    {
        // Si estamos en el último panel, cerrar el canvas
        if (indiceActual >= panelesInstrucciones.Length - 1)
        {
            canvasInstruccionesUI.SetActive(false);
            return;
        }

        // Ocultar el panel actual
        panelesInstrucciones[indiceActual].SetActive(false);

        // Incrementar el índice actual
        indiceActual++;

        // Mostrar el siguiente panel
        panelesInstrucciones[indiceActual].SetActive(true);

        // Actualizar el botón según la instrucción actual
        ActualizarBoton();
    }

    private void SaltarInstrucciones()
    {
        // Ocultar el canvas de instrucciones
        canvasInstruccionesUI.SetActive(false);
    }

    private void ActualizarBoton()
    {
        // Si es la última instrucción
        if (indiceActual == panelesInstrucciones.Length - 1)
        {
            // Cambiar el gráfico del botón a "OK"
            btnSiguiente.GetComponent<Image>().sprite = graficoOK;

            // Ocultar el botón de saltar
            btnSaltar.gameObject.SetActive(false);
        }
        else
        {
            // Cambiar el gráfico del botón a flecha
            btnSiguiente.GetComponent<Image>().sprite = graficoFlecha;

            // Mostrar el botón de saltar
            btnSaltar.gameObject.SetActive(true);
        }
    }

    private void DesactivarRaycastTargets(GameObject panel)
    {
        // Obtén todos los componentes UI que tengan la propiedad RaycastTarget
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

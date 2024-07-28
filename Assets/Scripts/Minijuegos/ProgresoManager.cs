using UnityEngine;
using UnityEngine.UI;

public class ProgresoManager : MonoBehaviour
{
    public Slider barraDeProgreso;

    public void ActualizarProgreso(int rondas)
    {
        barraDeProgreso.value = rondas;
    }
}
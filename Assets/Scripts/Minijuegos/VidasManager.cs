using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasManager : MonoBehaviour
{
    public Image[] vidasArray;
    public Sprite vidaLlena;
    public Sprite vidaVacia;

    private int vidas = 3;

    public void ActualizarVidas(int vidasRestantes)
    {
        vidas = vidasRestantes;

        for (int i = 0; i < vidasArray.Length; i++)
        {
            if (i < vidas)
            {
                vidasArray[i].sprite = vidaLlena;
            }
            else
            {
                vidasArray[i].sprite = vidaVacia;
            }
        }
    }
}

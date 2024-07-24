using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Button[] minijuegoBTN;
    // Start is called before the first frame update
    void Start()
    {
        int minijuego = PlayerPrefs.GetInt("minijuego", 2);

        for (int i = 0; i < minijuegoBTN.Length; i++)
        {
            if (i + 2 > minijuego)
            {
                minijuegoBTN[i].interactable = false;
            }
        }
    }

}

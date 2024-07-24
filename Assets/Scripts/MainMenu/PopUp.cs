using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;
    public GameObject popFather;

    public void ShowPopUp()
    {
        GameObject popUpSettings = Instantiate(popUp);
        RectTransform popUpRect = popUpSettings.GetComponent<RectTransform>();
        popUpRect.sizeDelta = popFather.GetComponent<RectTransform>().sizeDelta;
        popUpSettings.transform.SetParent(popFather.gameObject.transform, false);
    }
    public void ClosePopUp()
    {
        if (popUp != null)
        {
           Destroy(popUp);
        }
    }
}

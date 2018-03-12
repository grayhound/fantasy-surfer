using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumAccountInformationPopupOkButton : MonoBehaviour
{
    public GameObject canvas;
    public CanvasToggler canvasToggler;

    public void OnClickAction()
    {
        canvasToggler.HideMainCanvas(canvas);
    }
}

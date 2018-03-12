using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumAccountButton : MonoBehaviour
{
    public GameObject canvas;
    public CanvasToggler canvasToggler;

    public void OnClickAction()
    {
        canvasToggler.ShowMainCanvas(canvas);
    }
}

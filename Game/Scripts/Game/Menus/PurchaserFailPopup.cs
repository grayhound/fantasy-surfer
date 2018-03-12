using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaserFailPopup : MonoBehaviour {
    public CanvasToggler canvasToggler;
    public GameObject popupCanvas;

    public void ShowPopup()
    {
        canvasToggler.ShowMainCanvas(popupCanvas);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("purchaser_fail_popup");
    }

    public void HidePopup()
    {
        canvasToggler.HideMainCanvas(popupCanvas);
    }
    
}

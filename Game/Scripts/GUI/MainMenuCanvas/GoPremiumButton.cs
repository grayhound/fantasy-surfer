using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPremiumButton : MonoBehaviour
{
    public GoPremiumPopup goPremiumPopup;

    public void OnClickAction()
    {
        goPremiumPopup.ShowMainPopup();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("go_premium_popup_click");
    }    
}

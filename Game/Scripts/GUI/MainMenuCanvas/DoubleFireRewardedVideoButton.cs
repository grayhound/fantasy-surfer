using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFireRewardedVideoButton : MonoBehaviour {
    public GetDoubleFirePopup getDoubleFirePopup;

    public void OnClickAction()
    {
        getDoubleFirePopup.ShowMainPopup();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("double_fire_popup_click");
    }
}

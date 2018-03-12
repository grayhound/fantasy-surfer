using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShieldRewardedVideoButton : MonoBehaviour {
    public GetAutoShieldPopup getAutoShieldPopup;

    public void OnClickAction()
    {
        getAutoShieldPopup.ShowMainPopup();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("auto_shield_popup_click");
    }
}

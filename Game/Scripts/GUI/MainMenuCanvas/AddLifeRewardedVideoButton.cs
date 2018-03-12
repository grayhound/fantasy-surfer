using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLifeRewardedVideoButton : MonoBehaviour {
    public GetAdditionalLifePopup getAdditionalLifePopup;

    public void OnClickAction()
    {
        getAdditionalLifePopup.ShowMainPopup();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("add_life_popup_click");
    }
}

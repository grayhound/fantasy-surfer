using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateGamePopupRateNowButton : MonoBehaviour {
    public PleaseRate pleaseRate;

    public void OnClickAction()
    {
        pleaseRate.OpenRateNow();
        pleaseRate.HidePleaseRatePopup();
    }
}

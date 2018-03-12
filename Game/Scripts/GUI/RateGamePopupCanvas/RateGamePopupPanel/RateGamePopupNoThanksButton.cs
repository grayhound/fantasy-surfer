using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateGamePopupNoThanksButton : MonoBehaviour {
    public PleaseRate pleaseRate;

    public void OnClickAction()
    {
        pleaseRate.DisableRateNowForever();
        pleaseRate.HidePleaseRatePopup();
    }
}

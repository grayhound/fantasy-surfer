using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateGamePopupLaterButton : MonoBehaviour {
    public PleaseRate pleaseRate;

    public void OnClickAction()
    {
        pleaseRate.ResetRateNow();
        pleaseRate.HidePleaseRatePopup();
    }
}

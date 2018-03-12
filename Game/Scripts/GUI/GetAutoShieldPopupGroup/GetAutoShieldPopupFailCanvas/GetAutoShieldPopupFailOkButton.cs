using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAutoShieldPopupFailOkButton : MonoBehaviour {
    public GetAutoShieldPopup getAutoShieldPopup;

    public void OnClickAction()
    {
        getAutoShieldPopup.HideFailPopup();
    }
}

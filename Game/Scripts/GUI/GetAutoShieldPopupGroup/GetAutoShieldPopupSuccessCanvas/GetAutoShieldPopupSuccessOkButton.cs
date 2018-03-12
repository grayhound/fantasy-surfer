using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAutoShieldPopupSuccessOkButton : MonoBehaviour {
    public GetAutoShieldPopup getAutoShieldPopup;

    public void OnClickAction()
    {
        getAutoShieldPopup.HideSuccessPopup();
    }
}

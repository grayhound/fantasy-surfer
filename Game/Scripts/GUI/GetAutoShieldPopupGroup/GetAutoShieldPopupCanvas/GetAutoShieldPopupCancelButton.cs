using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAutoShieldPopupCancelButton : MonoBehaviour {
    public GetAutoShieldPopup getAutoShieldPopup;

    public void OnClickAction()
    {
        getAutoShieldPopup.HideMainPopup();
    }
}

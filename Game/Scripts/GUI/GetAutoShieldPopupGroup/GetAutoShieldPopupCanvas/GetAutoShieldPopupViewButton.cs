using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAutoShieldPopupViewButton : MonoBehaviour {
    public GetAutoShieldPopup getAutoShieldPopup;

    public void OnClickAction()
    {
        getAutoShieldPopup.ShowBanner();
    }
}

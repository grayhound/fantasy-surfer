using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoubleFirePopupFailOkButton : MonoBehaviour {
    public GetDoubleFirePopup getDoubleFirePopup;

    public void OnClickAction()
    {
        getDoubleFirePopup.HideFailPopup();
    }
}

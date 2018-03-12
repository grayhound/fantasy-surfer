using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoubleFirePopupSuccessOkButton : MonoBehaviour {
    public GetDoubleFirePopup getDoubleFirePopup;

    public void OnClickAction()
    {
        getDoubleFirePopup.HideSuccessPopup();
    }
}

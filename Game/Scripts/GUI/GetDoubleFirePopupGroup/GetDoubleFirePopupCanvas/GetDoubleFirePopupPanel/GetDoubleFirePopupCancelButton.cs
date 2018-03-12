using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoubleFirePopupCancelButton : MonoBehaviour {
    public GetDoubleFirePopup getDoubleFirePopup;

    public void OnClickAction()
    {
        getDoubleFirePopup.HideMainPopup();
    }
}

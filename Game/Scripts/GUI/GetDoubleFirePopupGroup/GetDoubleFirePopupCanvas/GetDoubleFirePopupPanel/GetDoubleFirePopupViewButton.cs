using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoubleFirePopupViewButton : MonoBehaviour {
    public GetDoubleFirePopup getDoubleFirePopup;

    public void OnClickAction()
    {
        getDoubleFirePopup.ShowBanner();
    }
}

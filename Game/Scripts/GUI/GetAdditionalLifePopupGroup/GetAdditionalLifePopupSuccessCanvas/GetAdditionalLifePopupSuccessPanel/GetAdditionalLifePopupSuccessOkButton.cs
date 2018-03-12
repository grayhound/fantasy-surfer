using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAdditionalLifePopupSuccessOkButton : MonoBehaviour {
    public GetAdditionalLifePopup getAdditionalLifePopup;

    public void OnClickAction()
    {
        getAdditionalLifePopup.HideSuccessPopup();
    }
}

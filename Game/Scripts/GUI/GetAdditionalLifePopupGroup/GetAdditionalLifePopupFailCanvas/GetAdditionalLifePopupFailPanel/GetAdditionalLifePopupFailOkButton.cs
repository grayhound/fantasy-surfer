using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAdditionalLifePopupFailOkButton : MonoBehaviour {
    public GetAdditionalLifePopup getAdditionalLifePopup;

    public void OnClickAction()
    {
        getAdditionalLifePopup.HideFailPopup();
    }
}

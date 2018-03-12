using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAdditionalLifePopupCancelButton : MonoBehaviour {
    public GetAdditionalLifePopup getAdditionalLifePopup;

    public void OnClickAction()
    {
        getAdditionalLifePopup.HideMainPopup();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAdditionalLifePopupViewButton : MonoBehaviour {
    public GetAdditionalLifePopup getAdditionalLifePopup;

    public void OnClickAction()
    {
        getAdditionalLifePopup.ShowBanner();
    }
}

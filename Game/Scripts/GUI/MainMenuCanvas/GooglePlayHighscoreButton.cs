using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayHighscoreButton : MonoBehaviour {
    public GooglePlayServices googlePlayServices;

    public void OnClick()
    {
        googlePlayServices.ShowHiscore();
    }
}

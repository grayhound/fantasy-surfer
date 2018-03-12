using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayAuthButton : MonoBehaviour {
    public GooglePlayServices googlePlayServices;

    public void OnClick()
    {
        googlePlayServices.AuthenticateUserStart();
    }
}

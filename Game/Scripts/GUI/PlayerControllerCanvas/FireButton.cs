using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour {
    public PlayerGun playerGun;

    public void OnPointerDownAction()
    {        
        playerGun.FireStart();
    }

    public void OnPointerUpAction()
    {        
        playerGun.FireStop();
    }
}

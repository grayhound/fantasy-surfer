using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaserFailPopupExitButton : MonoBehaviour
{
    public ExitGame exitGame;

    public void OnClickAction()
    {
        exitGame.ShowExitGameCanvas();    
    }    
}

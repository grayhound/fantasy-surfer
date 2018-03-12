using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaserFailPopupOkButton : MonoBehaviour
{
    public PurchaserFailPopup purchaserFailPopup;

    public void OnClickAction()
    {
        purchaserFailPopup.HidePopup(); 
    }    
}

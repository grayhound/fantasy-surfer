using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPremiumPopupFailOkButton : MonoBehaviour {
	public GoPremiumPopup goPremiumPopup;

	public void OnClickAction()
	{
		goPremiumPopup.HideFailPopup();
	}
}

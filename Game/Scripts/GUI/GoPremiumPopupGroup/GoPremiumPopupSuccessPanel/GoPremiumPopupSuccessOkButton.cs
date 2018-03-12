using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPremiumPopupSuccessOkButton : MonoBehaviour {
	public GoPremiumPopup goPremiumPopup;

	public void OnClickAction()
	{
		goPremiumPopup.HideSuccessPopup();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPremiumPopupCancelButton : MonoBehaviour {
	public GoPremiumPopup goPremiumPopup;

	public void OnClickAction()
	{
		goPremiumPopup.HideMainPopup();
	}
}

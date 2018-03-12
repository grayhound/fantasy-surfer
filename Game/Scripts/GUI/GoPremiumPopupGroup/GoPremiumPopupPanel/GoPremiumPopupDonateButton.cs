using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPremiumPopupDonateButton : MonoBehaviour {
	public GoPremiumPopup goPremiumPopup;

	public void OnClickAction()
	{
		goPremiumPopup.DonateAction();
	}
}

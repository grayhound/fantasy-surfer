using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupPanelCloseButton : MonoBehaviour {
	public Tutorial tutorial;

	public void OnClickAction()
	{
		tutorial.HidePanel();
	}
}

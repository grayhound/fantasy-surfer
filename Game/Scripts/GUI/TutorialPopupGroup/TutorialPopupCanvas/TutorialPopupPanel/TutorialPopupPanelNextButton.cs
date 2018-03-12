using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupPanelNextButton : MonoBehaviour {
	public Tutorial tutorial;

	public void OnClickAction()
	{
		tutorial.ShowNext();
	}
}

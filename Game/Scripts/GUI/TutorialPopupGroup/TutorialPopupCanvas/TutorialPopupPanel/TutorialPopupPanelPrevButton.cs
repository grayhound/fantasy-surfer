using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupPanelPrevButton : MonoBehaviour
{
    public Tutorial tutorial;

    public void OnClickAction()
    {
        tutorial.ShowPrev();
    }
}

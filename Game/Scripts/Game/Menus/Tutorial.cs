using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public CanvasToggler canvasToggler;
    public GameObject tutorialCanvas;
    
    public GameObject[] tutorialPopupContents;
    
    public GameObject nextButton;
    public GameObject prevButton;

    private int contentIndex = 0;

    public void ShowPanel()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("tutorial_popup_show");
        canvasToggler.ShowMainCanvas(tutorialCanvas);
        contentIndex = 0;
        ShowContent();
    }

    public void HidePanel()
    {   
        canvasToggler.HideMainCanvas(tutorialCanvas);
    }

    private void ShowContent()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("tutorial_popup_show_content_" + contentIndex.ToString());
        for (int i = 0; i < tutorialPopupContents.Length; i++) {
            if (i == contentIndex) {
                tutorialPopupContents[i].SetActive(true);
            } else {
                tutorialPopupContents[i].SetActive(false);
            }
        }
        CheckButtons();
    }
    
    public void ShowNext()
    {
        contentIndex++;
        if (IsLastContent()) {
            contentIndex = tutorialPopupContents.Length - 1;
        }
        ShowContent();
    }

    public void ShowPrev()
    {
        contentIndex--;
        if (IsFirstContent()) {
            contentIndex = 0;
        }
        ShowContent();
    }

    private void CheckButtons()
    {
        nextButton.SetActive(true);
        prevButton.SetActive(true);
        if (IsFirstContent()) {
            prevButton.SetActive(false);
        }
        if (IsLastContent()) {
            nextButton.SetActive(false);
        }        
    }

    private bool IsLastContent()
    {
        return contentIndex >= tutorialPopupContents.Length - 1;
    }

    private bool IsFirstContent()
    {
        return contentIndex <= 0;
    }
    
}

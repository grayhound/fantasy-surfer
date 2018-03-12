using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoPremiumPopup : MonoBehaviour {
    public Purchaser purchaser;

    public GameObject popupCanvasObject;
    public GameObject popupSuccessCanvasObject;
    public GameObject popupFailCanvasObject;
    public ExitGame exitGame;

    public GameObject donateButton;

    public Text failErrorText;

    public CanvasToggler canvasToggler;
    public GameObject goPremiumButton;

    private bool isDisabled = false;
    
    public void Enable()
    {
        isDisabled = false;
        goPremiumButton.SetActive(true);
    }

    public void Disable()
    {
        isDisabled = true;
        goPremiumButton.SetActive(false);
    }    
    
    public void ShowMainPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        exitGame.Disable();
        canvasToggler.ShowMainCanvas(popupCanvasObject);
    }

    public void HideMainPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        exitGame.Enable();
        canvasToggler.HideMainCanvas(popupCanvasObject);
    }

    public void ShowSuccessPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        canvasToggler.ShowMainCanvas(popupSuccessCanvasObject);
    }

    public void HideSuccessPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        canvasToggler.HideMainCanvas(popupSuccessCanvasObject);
    }

    public void ShowFailPopup(string errorText)
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        failErrorText.text = errorText;
        canvasToggler.ShowMainCanvas(popupFailCanvasObject);
    }

    public void HideFailPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        canvasToggler.HideMainCanvas(popupFailCanvasObject);
    }

    public void DonateAction()
    {
        purchaser.BuyPremiumAccount();
    }

    private void UpdateViewButton()
    {
        /*
        if (playerLivesBonusGames >= PlayerGun.PLAYER_MAX_POSSIBLE_DOUBLE_FIRE_BONUS_GAMES) {
            viewButton.SetActive(false);
        } else {
            viewButton.SetActive(true);
        }
        */
    }

    private void UpdateGamesLeftText()
    {        
    }
}

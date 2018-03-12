using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAdditionalLifePopup : MonoBehaviour {
    public AdMobAddLifeRewardedVideo adMobAddLifeRewardedVideo;

    public GameObject popupCanvasObject;
    public GameObject popupSuccessCanvasObject;
    public GameObject popupFailCanvasObject;
    public ExitGame exitGame;

    public Image[] demoHearts;
    public GameObject maximumLivesDescriptionText;

    public GameObject viewButton;
    public Text gamesLeftText;

    public GameObject normalDescriptionText;

    public CanvasToggler canvasToggler;

    public Player player;

    private string gamesLeftTextTemplate = "{0} games left";

    private Color32 demoHeartFull = new Color32(255, 255, 255, 255);
    private Color32 demoHeartEmpty = new Color32(255, 255, 255, 30);

    void Start ()
    {		
	}
	
	void Update ()
    {		
	}

    public void ShowMainPopup()
    {
        UpdateDemoHearts();
        UpdateViewButton();
        UpdateGamesLeftText();
        UpdateDescriptionText();

        exitGame.Disable();
        canvasToggler.ShowMainCanvas(popupCanvasObject);
        adMobAddLifeRewardedVideo.PrepareEvents();
    }

    public void HideMainPopup()
    {
        UpdateDemoHearts();
        UpdateViewButton();
        UpdateGamesLeftText();
        UpdateDescriptionText();

        exitGame.Enable();
        canvasToggler.HideMainCanvas(popupCanvasObject);
        adMobAddLifeRewardedVideo.ClearEvents();
    }

    public void ShowSuccessPopup()
    {
        UpdateDemoHearts();
        UpdateViewButton();
        UpdateGamesLeftText();
        UpdateDescriptionText();

        canvasToggler.ShowMainCanvas(popupSuccessCanvasObject);
    }

    public void HideSuccessPopup()
    {
        UpdateDemoHearts();
        UpdateViewButton();
        UpdateGamesLeftText();
        UpdateDescriptionText();

        canvasToggler.HideMainCanvas(popupSuccessCanvasObject);
    }

    public void ShowFailPopup()
    {
        UpdateDemoHearts();
        UpdateViewButton();
        UpdateGamesLeftText();
        UpdateDescriptionText();

        canvasToggler.ShowMainCanvas(popupFailCanvasObject);
    }

    public void HideFailPopup()
    {
        UpdateDemoHearts();
        UpdateViewButton();
        UpdateGamesLeftText();
        UpdateDescriptionText();

        canvasToggler.HideMainCanvas(popupFailCanvasObject);
    }

    public void ShowBanner()
    {
        adMobAddLifeRewardedVideo.ShowBanner();
    }

    private void UpdateDemoHearts()
    {
        int maxLives = player.GetMaxPlayerLives();        
        for (int i = 0; i < Player.PLAYER_MAX_POSSIBLE_LIVES; i++) {
            if (i < maxLives) {
                demoHearts[i].color = demoHeartFull;
            } else {
                demoHearts[i].color = demoHeartEmpty;                               
            }
        }
    }

    private void UpdateViewButton()
    {
        int maxLives = player.GetMaxPlayerLives();
        if (maxLives >= Player.PLAYER_MAX_POSSIBLE_LIVES) {
            viewButton.SetActive(false);
        } else {
            viewButton.SetActive(true);
        }
    }

    private void UpdateGamesLeftText()
    {        
        int playerLivesBonusGames = player.GetPlayerLivesBonusGames();
        if (playerLivesBonusGames <= 0) {
            gamesLeftText.text = "";
        } else {
            gamesLeftText.text = string.Format(gamesLeftTextTemplate, playerLivesBonusGames.ToString());
        }
    }

    private void UpdateDescriptionText()
    {
        int maxLives = player.GetMaxPlayerLives();
        if (maxLives >= Player.PLAYER_MAX_POSSIBLE_LIVES) {
            maximumLivesDescriptionText.SetActive(true);
            normalDescriptionText.SetActive(false);
        } else {
            maximumLivesDescriptionText.SetActive(false);
            normalDescriptionText.SetActive(true);
        }
    }
}

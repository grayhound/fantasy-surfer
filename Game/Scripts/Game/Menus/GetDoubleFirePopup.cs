using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDoubleFirePopup : MonoBehaviour {
    public AdMobDoubleFireRewardedVideo adMobDoubleFireRewardedVideo;

    public GameObject popupCanvasObject;
    public GameObject popupSuccessCanvasObject;
    public GameObject popupFailCanvasObject;
    public ExitGame exitGame;

    public GameObject viewButton;
    public Text gamesLeftText;

    public CanvasToggler canvasToggler;

    public PlayerGun playerGun;

    private string gamesLeftTextTemplate = "{0} games left";

    void Start()
    {
    }

    void Update()
    {
    }

    public void ShowMainPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        exitGame.Disable();
        canvasToggler.ShowMainCanvas(popupCanvasObject);
        adMobDoubleFireRewardedVideo.PrepareEvents();
    }

    public void HideMainPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        exitGame.Enable();
        canvasToggler.HideMainCanvas(popupCanvasObject);
        adMobDoubleFireRewardedVideo.ClearEvents();
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

    public void ShowFailPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        canvasToggler.ShowMainCanvas(popupFailCanvasObject);
    }

    public void HideFailPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        canvasToggler.HideMainCanvas(popupFailCanvasObject);
    }

    public void ShowBanner()
    {
        adMobDoubleFireRewardedVideo.ShowBanner();
    }

    private void UpdateViewButton()
    {
        int playerLivesBonusGames = playerGun.GetDoubleFireBonusGames();
        if (playerLivesBonusGames >= PlayerGun.PLAYER_MAX_POSSIBLE_DOUBLE_FIRE_BONUS_GAMES) {
            viewButton.SetActive(false);
        } else {
            viewButton.SetActive(true);
        }
    }

    private void UpdateGamesLeftText()
    {
        int playerLivesBonusGames = playerGun.GetDoubleFireBonusGames();
        if (playerLivesBonusGames <= 0) {
            gamesLeftText.text = "";
        } else {
            gamesLeftText.text = string.Format(gamesLeftTextTemplate, playerLivesBonusGames.ToString());
        }
        
    }
}

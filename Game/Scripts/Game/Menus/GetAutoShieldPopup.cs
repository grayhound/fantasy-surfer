using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAutoShieldPopup : MonoBehaviour {
    public AdMobAutoShieldRewardedVideo adMobAutoShieldRewardedVideo;

    public GameObject popupCanvasObject;
    public GameObject popupSuccessCanvasObject;
    public GameObject popupFailCanvasObject;
    public ExitGame exitGame;

    public GameObject viewButton;
    public Text gamesLeftText;

    public CanvasToggler canvasToggler;

    public Player player;

    private string gamesLeftTextTemplate = "{0} games left";

    public void ShowMainPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        exitGame.Disable();
        canvasToggler.ShowMainCanvas(popupCanvasObject);
        adMobAutoShieldRewardedVideo.PrepareEvents();
    }

    public void HideMainPopup()
    {
        UpdateViewButton();
        UpdateGamesLeftText();

        exitGame.Enable();
        canvasToggler.HideMainCanvas(popupCanvasObject);
        adMobAutoShieldRewardedVideo.ClearEvents();
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
        adMobAutoShieldRewardedVideo.ShowBanner();
    }

    private void UpdateViewButton()
    {
        int playerLivesBonusGames = player.GetAutoShieldBonusGames();
        if (playerLivesBonusGames >= Player.PLAYER_MAX_POSSIBLE_AUTO_SHIELD_BONUS_GAMES) {
            viewButton.SetActive(false);
        } else {
            viewButton.SetActive(true);
        }
    }

    private void UpdateGamesLeftText()
    {
        int playerLivesBonusGames = player.GetAutoShieldBonusGames();
        if (playerLivesBonusGames <= 0) {
            gamesLeftText.text = "";
        } else {
            gamesLeftText.text = string.Format(gamesLeftTextTemplate, playerLivesBonusGames.ToString());
        }

    }
}

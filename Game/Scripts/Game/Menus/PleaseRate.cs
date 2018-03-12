using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseRate : MonoBehaviour
{
    public static string PLEASE_RATE_GAMES_PLAYED_SAVENAME = "PleaseRateGamesPlayed";
    public static string PLEASE_RATE_IS_ENABLED_SAVENAME = "PleaseRateIsEnabled";

    public GameObject pleaseRateCanvas;
    public CanvasToggler canvasToggler;

    private int gamesPlayed = 0;
    private int isEnabled = 1;

    private int gamesToPlay = 3;

    private string ratingUrl = "market://details?id={0}";

    void Start()
    {
        LoadGamesPlayed();
    }

    void Update()
    {
    }

    private void LoadGamesPlayed()
    {
        gamesPlayed = PlayerPrefs.GetInt(PLEASE_RATE_GAMES_PLAYED_SAVENAME, 0);
        isEnabled = PlayerPrefs.GetInt(PLEASE_RATE_IS_ENABLED_SAVENAME, 1);
    }

    public void SaveGamesPlayed()
    {
        PlayerPrefs.SetInt(PLEASE_RATE_GAMES_PLAYED_SAVENAME, gamesPlayed);
        PlayerPrefs.SetInt(PLEASE_RATE_IS_ENABLED_SAVENAME, isEnabled);
    }

    public void AddGamesPlayed()
    {
        gamesPlayed++;
        if (gamesPlayed >= 5) {
            gamesPlayed = 5;
        }
        SaveGamesPlayed();
    }

    public void CheckPleaseRatePopup()
    {
        if (gamesPlayed >= gamesToPlay && isEnabled == 1) {
            canvasToggler.ShowMainCanvas(pleaseRateCanvas);
        }
    }

    public void OpenRateNow()
    {
        string url = string.Format(ratingUrl, Application.identifier);
        Application.OpenURL(url);
        DisableRateNowForever();
    }

    public void DisableRateNowForever()
    {        
        isEnabled = 0;
        SaveGamesPlayed();
    }

    public void ResetRateNow()
    {
        gamesPlayed = 0;
        SaveGamesPlayed();
    }

    public void HidePleaseRatePopup()
    {
        canvasToggler.HideMainCanvas(pleaseRateCanvas);
    }
}

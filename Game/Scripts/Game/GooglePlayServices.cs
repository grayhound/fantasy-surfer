using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour {
    public GameObject googlePlayAuthButton;
    public GameObject googlePlayHighscoreButton;

    private bool previousIsAuthenticated = false;
    
    void Awake()
    {
        EnableGooglePlayServices();
    }

    void Start()
    {
        HideAll();
        AuthenticateUserStart();
	}

    void Update()
    {
        CheckUserIsAuthenticated();
    }

    public void HideAll()
    {
        googlePlayAuthButton.SetActive(false);
        googlePlayHighscoreButton.SetActive(false);        
    }

    public void EnableGooglePlayServices()
    {
        GooglePlayGames.PlayGamesPlatform.Activate();
    }

    public void AuthenticateUserStart()
    {
        Social.localUser.Authenticate((bool success) => {            
            if (success) {                
                AuthenticationSuccess();
            } else {                
                AuthenticationFailure();
            }
        });
    }

    public void AuthenticationSuccess()
    {
        googlePlayAuthButton.SetActive(false);
        googlePlayHighscoreButton.SetActive(true);
    }

    public void AuthenticationFailure()
    {
        googlePlayAuthButton.SetActive(true);
        googlePlayHighscoreButton.SetActive(false);
    }

    public void AuthenticateUserHiscore()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success) {
                AuthenticationSuccess();
                ShowRealHiscore();
            } else {
                AuthenticationFailure();
            }
        });
    }

    public void ShowRealHiscore()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_hiscore);
    }

    public void ShowHiscore()
    {
        if (Social.localUser.authenticated) {
            ShowRealHiscore();
        } else {
            AuthenticateUserHiscore();
        }
    }

    public void SendHiscore(long newScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_hiscore, (bool success) => {});        
    }

    private void CheckUserIsAuthenticated()
    {
        if (Social.localUser.authenticated && !previousIsAuthenticated) {
            AuthenticationSuccess();
            previousIsAuthenticated = true;
        }
        if (!Social.localUser.authenticated && previousIsAuthenticated) {
            AuthenticationFailure();
            previousIsAuthenticated = false;
        }        
    }
    
}

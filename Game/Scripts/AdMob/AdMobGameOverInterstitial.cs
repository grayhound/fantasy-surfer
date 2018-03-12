using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobGameOverInterstitial : MonoBehaviour {
    public bool isTest = true;

    private InterstitialAd interstitial;
    private string adUnitId;

    private bool isDisabled = false;
    
    private void Start ()
    {
        if (isTest) {
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        } else {
            adUnitId = "ca-app-pub-9115186499993249/4348803856";
        }        
    }	

    public void PrepareBanner()
    {
        if (isDisabled) {
            return;
        }        
        AdRequest request = new AdRequest.Builder().Build();               
        interstitial.LoadAd(request);        
    }

    public void ShowBanner()
    {
        if (isDisabled) {
            return;
        }
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        }
    }

    public void Enable()
    {
        isDisabled = false;
        interstitial = new InterstitialAd(adUnitId);
    }

    public void Disable()
    {
        isDisabled = true;
    }
}

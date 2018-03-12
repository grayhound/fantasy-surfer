using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobBottomBanner : MonoBehaviour {       
    public bool isTest = true;

    private BannerView bannerView;
    private string adUnitId;

    private bool isDisabled = false;

    private float densityDefault = 160.0f;
    
    void Start ()
    {
        if (isTest) {
            adUnitId = "ca-app-pub-3940256099942544/6300978111";
        } else {
            adUnitId = "ca-app-pub-9115186499993249/7260115647";
        }        
	}

    public void Disable()
    {
        HideBanner();
        isDisabled = true;
    }

    public void Enable()
    {
        isDisabled = false;
        PrepareBanner();
        ShowBanner();        
    }    

    public void PrepareBanner()
    {
        AdSize useAdSize = AdSize.Banner;
        float widthDP = Screen.width / (Screen.dpi / densityDefault);
        
        
        if (widthDP >= AdSize.Banner.Width) {
            useAdSize = AdSize.Banner;
        }        
        if (widthDP >= AdSize.IABBanner.Width) {
            useAdSize = AdSize.IABBanner;
        }        
        if (widthDP >= AdSize.Leaderboard.Width) {
            useAdSize = AdSize.Leaderboard;
        }
      
        bannerView = new BannerView(adUnitId, useAdSize, AdPosition.Bottom);
    }

    public void ShowBanner()
    {
        if (isDisabled) {
            return;
        }
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        bannerView.Show();
    }

    public void HideBanner()
    {
        if (isDisabled) {
            return;
        }        
        bannerView.Hide();
    }
}

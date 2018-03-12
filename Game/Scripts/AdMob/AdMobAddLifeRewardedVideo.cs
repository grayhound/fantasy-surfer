using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobAddLifeRewardedVideo : MonoBehaviour {
    public bool isTest = true;
    public CanvasToggler canvasToggler;
    public GetAdditionalLifePopup getAdditionalLifePopup;
    public Player player;
    public GameObject addLifeRewardedVideoButton;

    private RewardBasedVideoAd rewardBasedVideo;
    private string adUnitId;

    private bool _isVideoShow = false;
    private bool _hideBanner = false;

    private bool _fakeSuccess = false;
    private bool _fakeFail = false;

    private bool isDisabled = false;

    void Start ()
    {        
        if (isTest) {
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        } else {
            adUnitId = "ca-app-pub-9115186499993249/2985828717";
        }
    }

    void Update ()
    {
        if (isDisabled) {
            return;
        }
        CheckFakeHideBanner();
        CheckFakeSuccess();
        CheckFakeFail();
    }

    public void Enable()
    {
        isDisabled = false;
        addLifeRewardedVideoButton.SetActive(true);
    }

    public void Disable()
    {
        isDisabled = true;
        addLifeRewardedVideoButton.SetActive(false);
    }    

    public void PrepareEvents()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        ClearEvents();

        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
    }

    public void ShowBanner()
    {        
        canvasToggler.ShowPleaseWait();
        RequestRewardedVideo();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("add_life_popup_show_banner");
    }

    private void RequestRewardedVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {        
        FakeSuccess();
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        FakeHideBanner();
        FakeFail();
    }

    public void HandleRewardBasedVideoLoaded(object sender, System.EventArgs args)
    {
        _isVideoShow = true;
        rewardBasedVideo.Show();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("add_life_popup_show_banner_loaded");
    }

    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {        
        FakeHideBanner();
    }

    public void FakeHideBanner()
    {
        _isVideoShow = false;
        _hideBanner = true;
    }

    public void FakeSuccess()
    {
        _fakeSuccess = true;
    }

    public void FakeFail()
    {
        _fakeFail = true;
    }

    public void CheckFakeHideBanner()
    {
        if (!_isVideoShow && _hideBanner) {
            _isVideoShow = false;
            _hideBanner = false;
            HideBanner();           
        }
    }

    public void CheckFakeSuccess()
    {
        if (_fakeSuccess) {
            HideBanner();
            player.AddLifeBonus();
            _fakeSuccess = false;
            getAdditionalLifePopup.ShowSuccessPopup();
            Firebase.Analytics.FirebaseAnalytics.LogEvent("add_life_popup_success");
        }
    }

    public void CheckFakeFail()
    {
        if (_fakeFail) {
            _fakeFail = false;
            getAdditionalLifePopup.ShowFailPopup();
            Firebase.Analytics.FirebaseAnalytics.LogEvent("add_life_popup_fail");
        }
    }

    public void HideBanner(bool isError = false)
    {
        canvasToggler.HidePleaseWait();        
    }

    public void ClearEvents()
    {
        rewardBasedVideo.OnAdLoaded -= HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
    public GoPremiumPopup goPremiumPopup;
    public CanvasToggler canvasToggler;
    public PurchaserFailPopup purchaserFailPopup;

    public AdMobBottomBanner adMobBottomBanner;
    public AdMobGameOverInterstitial adMobGameOverInterstitial;
    public AdMobAddLifeRewardedVideo adMobAddLifeRewardedVideo;
    public AdMobDoubleFireRewardedVideo adMobDoubleFireRewardedVideo;
    public AdMobAutoShieldRewardedVideo adMobAutoShieldRewardedVideo;

    public Player player;
    public PlayerGun playerGun;

    public GameObject premiumAccountTextGameObject;
    
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider; 

    public static string kProductIDPremiumAccount = "premium_account";

    private bool isPremium = false;
         
    void Start()
    {
        if (m_StoreController == null) {
            InitializePurchasing();
        }
    }
    
    public void InitializePurchasing() 
    {
        if (IsInitialized()) {
            return;
        }
        canvasToggler.ShowPleaseWait();
        
        // Create builder and add premium account product
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());       
        builder.AddProduct(kProductIDPremiumAccount, ProductType.NonConsumable);        
        UnityPurchasing.Initialize(this, builder);
    }
    
    
    private bool IsInitialized()
    {        
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }    
    
    public void BuyPremiumAccount()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("go_premium_popup_purchase_click");
        BuyProductID(kProductIDPremiumAccount);
    }       
    
    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            
            if (product != null && product.availableToPurchase)
            {
                //Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                //Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            //Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }    
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        canvasToggler.HidePleaseWait();
        CheckPremiumAccount();
    }
    
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //Debug.Log("IAP init error: " + error.ToString());
        canvasToggler.HidePleaseWait();
        goPremiumPopup.Disable();
        purchaserFailPopup.ShowPopup();
        CheckPremiumAccount();
    }
    
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
    {
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDPremiumAccount, StringComparison.Ordinal)) {            
            goPremiumPopup.HideMainPopup();
            goPremiumPopup.ShowSuccessPopup();
            CheckPremiumAccount();
        }
 
        return PurchaseProcessingResult.Complete;
    }
    
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        goPremiumPopup.ShowFailPopup(failureReason.ToString());
        Firebase.Analytics.FirebaseAnalytics.LogEvent("go_premium_popup_purchase_fail");
    }

    public void CheckPremiumAccount()
    {
        if (!IsInitialized()) {
            DisablePremium();
            return;
        }
        bool isPurchased = m_StoreController.products.WithID(kProductIDPremiumAccount).hasReceipt;        
        if (isPurchased) {
            EnablePremium();
        } else {
            DisablePremium();
        }
    }

    public void EnablePremium()
    {
        isPremium = true;
        premiumAccountTextGameObject.SetActive(true);
        adMobBottomBanner.Disable();
        adMobGameOverInterstitial.Disable();
        adMobAddLifeRewardedVideo.Disable();
        adMobDoubleFireRewardedVideo.Disable();
        adMobAutoShieldRewardedVideo.Disable();
        player.EnableMaxLives();
        player.EnableAlwaysAutoshield();
        playerGun.EnableAlwaysDoubleFire();
        goPremiumPopup.Disable();
    }

    public void DisablePremium()
    {
        isPremium = false;
        premiumAccountTextGameObject.SetActive(false);
        adMobBottomBanner.Enable();
        adMobGameOverInterstitial.Enable();
        adMobAddLifeRewardedVideo.Enable();
        adMobDoubleFireRewardedVideo.Enable();
        adMobAutoShieldRewardedVideo.Enable();
        goPremiumPopup.Enable();
        player.DisableMaxLives();
        player.DisableAlwaysAutoshield();
        playerGun.DisableAlwaysDoubleFire();
    }

    public bool GetIsPremium()
    {
        return isPremium;
    }
}
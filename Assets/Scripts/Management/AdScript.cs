using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;


/// The main ad script
public class AdScript : MonoBehaviour
{
    enum Status
    {
        test,
        release
    }
    public InterstitialAd interstitial_Ad;
    public RewardedAd rewardedAd;
    [SerializeField] private Status adStatus;

    [SerializeField] private string interstitial_Ad_ID;
    [SerializeField] private string rewardedAd_ID;
    [SerializeField] private string bannerAd_ID;

    private BannerView bannerView;
    private String rewardedAdIsFor = "star";


    void Start()
    {

        if (adStatus == Status.test)
        {//Set all ids to test ads

            interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712";
            rewardedAd_ID = "ca-app-pub-3940256099942544/5224354917";
            bannerAd_ID = "	ca-app-pub-3940256099942544/6300978111";
            Debug.Log("add units are changed to test ads");

        }
        //Set For Designed For Families
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent.True)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        
        
        MobileAds.Initialize(initStatus => { });

        RequestInterstitial();
        RequestRewardedVideo();

        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = bannerAd_ID;
#elif UNITY_IPHONE
            string adUnitId = bannerAd_ID;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }


    private void RequestInterstitial()
    {
        interstitial_Ad = new InterstitialAd(interstitial_Ad_ID);
        interstitial_Ad.OnAdLoaded += HandleOnAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial_Ad.LoadAd(request);
    }

    private void RequestRewardedVideo()
    {
        rewardedAd = new RewardedAd(rewardedAd_ID);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial_Ad.IsLoaded())
        {
            interstitial_Ad.Show();
            RequestInterstitial();
        }
    }

    public void ShowRewardedVideo(bool isForStar)
    {
        if (rewardedAd.IsLoaded())
        {
            if (isForStar)
            {
                rewardedAdIsFor = "star";
            }
            else
            {
                rewardedAdIsFor = "player";
            }

            rewardedAd.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        RequestRewardedVideo();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewardedVideo();
        if (rewardedAdIsFor.Equals("star"))
        {
            FindObjectOfType<GameSession>().giveStarReward();
        }
        else
        {
            FindObjectOfType<Player>().doubleThePlayer();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        RequestRewardedVideo();
    }
}
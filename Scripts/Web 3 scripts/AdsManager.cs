using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

  

public class AdsManager : MonoBehaviour,IUnityAdsInitializationListener,IUnityAdsShowListener,IUnityAdsLoadListener
{
    string placementAdUnit = "Rewarded_Android";
    public Button WatchAdButton;

    public int TotalCoins;

    void  Start()
    {
        //  Advertisement.AddListener(this);
        if (Advertisement.isInitialized)
        {
            Debug.Log("Ads is initialized");
            LoadAd();
        }
        else
        {
            InitializeAds();
        }
        TotalCoins = PlayerPrefs.GetInt("Coins");
      
    }
    public void InitializeAds()
    {
        Advertisement.Initialize("4837705", true, this);
    }
    public void LoadAd()
    {
        // loading after initialization
        Debug.Log("Loading Ad:" + placementAdUnit);
        Advertisement.Load(placementAdUnit, this);
    }
    
    public void ShowAd(string placement)
    {
        Advertisement.Show(placement,this);
    }
    
    
    public void OnUnityAdsReady(string placementId)
    {
       // throw new System.NotImplementedException();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity ads inititialization complete");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity ads inititialization failed:{error.ToString()} -{message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Unity ads show failed:{error.ToString()} -{message}");
    }

   
    

    public void OnUnityAdsAdLoaded(string placementId)
    {
      //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //  throw new System.NotImplementedException();
        Time.timeScale = 0; // to stop errors 
    }

    public void OnUnityAdsShowClick(string placementId)
    {
      //  throw new System.NotImplementedException();
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("You gained 10 coins");
        TotalCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", TotalCoins + 10);
        WatchAdButton.interactable = false;
        WatchAdButton.gameObject.SetActive(false);
        if (showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("You gained 50 coins");
            //  PlayerPrefs.SetInt("Coins", TotalCoins + 50);
            //  ScoreManager.instance.rewardplayer();
            //  ScoreManager.instance.SaveCoins();

            PlayerPrefs.SetInt("Coins", TotalCoins + 10);
            Debug.Log("You gained 10 coins");
            // reward player
        }
        Time.timeScale = 1;
    }
}

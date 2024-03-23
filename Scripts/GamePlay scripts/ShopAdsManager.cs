using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;



public class ShopAdsManager : MonoBehaviour//, IUnityAdsListener
{
    string placement = "Rewarded_Android";

    public StoreAdCoinManager manager;


    void Start()
    {
       // Advertisement.AddListener(this);
        Advertisement.Initialize("4837705", true);

    }
    public void ShowAd(string placement)
    {
        Advertisement.Show(placement);
    }

    public void OnUnityAdsDidError(string message)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            manager.SaveCoins();
          //  PlayerPrefs.SetInt("Coins", coinsStoring.instance.CoinsStored + 50);
        }
        else if (showResult == ShowResult.Failed)
        {
            // dont reward player
        }
        //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        // throw new System.NotImplementedException();
    }
    // Update is called once per frame
    void Update()
    {

    }
}


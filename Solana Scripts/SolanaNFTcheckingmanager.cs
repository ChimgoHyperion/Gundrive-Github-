using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolanaNFTcheckingmanager : MonoBehaviour
{
    public ShopManager shopManager;
    public bool hasMinted;
    public GameObject TransactionPanel;
    public bool shouldShowPanel = false;
    // Start is called before the first frame update
    void Start()
    {
        checkifnftisminted();
        if (PlayerPrefs.GetInt("HasMinted") == 1)
        {
            hasMinted = true;
        }
        else
        {
            hasMinted = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void confirmMint()
    {
        PlayerPrefs.SetInt("HasMinted", 1);
        hasMinted = true;
    }
    public void GotoNFTSite()
    {
        Application.OpenURL("https://www.launchmynft.co/#step1");
        shouldShowPanel = true;
    }
    public void checkifnftisminted()
    {
       
        if (hasMinted)
        {
            RetrieveSkinswithNFT();
        }
    }
    public void RetrieveSkinswithNFT()
    {
        shopManager.buttonStates.skin2Unlocked = true;
        shopManager.buttonStates.skin3Unlocked = true;
        shopManager.buttonStates.skin4Unlocked = true;
        shopManager.buttonStates.skin5Unlocked = true;
        shopManager.buttonStates.skin6Unlocked = true;
        shopManager.buttonStates.skin7Unlocked = true;
        shopManager.RerenderShop();
        shopManager.SaveJson();
    }
    private void OnApplicationPause(bool pauseStatus)// player has been redirected to icpswap
    {
        // isPaused = pauseStatus; // ispaused = true
        confirmMint();
    }

    private void OnApplicationFocus(bool hasfocus) // player has returned to the app
    {
        if (shouldShowPanel)
        {
            StartCoroutine(wait());
            checkifnftisminted();

        }

    }
    IEnumerator wait()
    {
        TransactionPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        TransactionPanel.SetActive(false);

    }
}

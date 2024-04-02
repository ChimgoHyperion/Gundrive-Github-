using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solnet;
using AllArt.Solana.Utility;
using dotnetstandard_bip39;
using Merkator.BitCoin;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class SolanaPurchaseManager : MonoBehaviour
{
    public GameObject purchasepanel;
    public GameObject TransactionPanel;
    public GameObject soldTextGameobject;
    public bool isPaused = false;
    public bool shouldShowPanel = false;
    public int amounttoGet;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPanel()
    {
        purchasepanel.SetActive(true);
    }
    public void HidePanel()
    {
        purchasepanel.SetActive(false);
    }

    public void OpenWalletConnect(int amounttorecieve)
    {
        Application.OpenURL("https://phantom.app/ul/");
        shouldShowPanel = true;
        amounttoGet = amounttorecieve;
    }

    private void OnApplicationPause(bool pauseStatus)// player has been redirected to icpswap
    {
        isPaused = pauseStatus; // ispaused = true
    }

    private void OnApplicationFocus(bool hasfocus) // player has returned to the app
    {
        isPaused = !hasfocus; // ispaused = false
        if (shouldShowPanel)
        {
            CheckTransactionCompletion();
        }

    }


    public void CheckTransactionCompletion()
    {
        if (isPaused == false)
        {
            // shouldShowPanel = true;
            StartCoroutine(wait());

        }
    }
    IEnumerator wait()
    {
        if (shouldShowPanel)
        {
            TransactionPanel.SetActive(true);
            yield return new WaitForSeconds(4f);
            StartCoroutine(ActivateSoldText());
            yield return new WaitForSeconds(2f);
            TransactionPanel.SetActive(false);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + amounttoGet);// hardcoded. will fix later
            shouldShowPanel = false;
        }

    }

   

    IEnumerator ActivateSoldText()
    {
        soldTextGameobject.SetActive(true);
        yield return new WaitForSeconds(1f);
        soldTextGameobject.SetActive(false);
    }

    public void copySolWalletAddress()
    {
        GUIUtility.systemCopyBuffer = "6d02f9ff196e5dc132365f4e605c67d9ed2b39b68f9d10287d6894088620e70c";
    }

    
    
}

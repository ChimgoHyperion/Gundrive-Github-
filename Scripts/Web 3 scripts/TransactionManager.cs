using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class TransactionManager : MonoBehaviour
{
    public GameObject InputPassCodePanel;
    public GameObject SentText;
    public InputField passcode,amounttosend;
    public int coinstored;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        coinstored = PlayerPrefs.GetInt("Coins");
    }

    // Update is called once per frame
    void Update()
    {
        amount = int.Parse(amounttosend.text);
        coinstored = PlayerPrefs.GetInt("Coins");
    }

    public void RevealPasscode()
    {
        InputPassCodePanel.SetActive(true);
    }

    public void SendToken()
    {
        if (passcode.text.Length == 4)
        {
            
           
            if (amount <= coinstored)
            {
                SentText.SetActive(true);
                int newcoins = coinstored - amount;
                PlayerPrefs.SetInt("Coins", newcoins);
                StartCoroutine(Wait());
            }
           
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        
        SceneManager.LoadScene("Main menu");
    }

    // playfab functions
    // 1. to send my earned coins to the server, 2. fuction to get coins from server when trying to send to someone
    // 3. function to transfer an amount sender wishes to receiver if the amount is less than what is in the server


        //  to transfer currency
    public void TransferCurrency(string recipientId, int amount)
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "TransferCurrency", // Name of your server-side CloudScript function
            FunctionParameter = new
            {
                recipient = recipientId,
                amount = amount
            }
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnTransferCurrencySuccess, OnTransferCurrencyFailure);
    }

    private void OnTransferCurrencySuccess(ExecuteCloudScriptResult result)
    {
        Debug.Log("Currency transfer successful!");
    }

    private void OnTransferCurrencyFailure(PlayFabError error)
    {
        Debug.LogError("Currency transfer failed: " + error.ErrorMessage);
    }
}

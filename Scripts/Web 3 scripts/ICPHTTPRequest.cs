using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ICPHTTPRequest : MonoBehaviour
{
    //  public TextAsset json;
    
    public string gamewallet = "6d02f9ff196e5dc132365f4e605c67d9ed2b39b68f9d10287d6894088620e70c"; // search string
    public string userwallet;
    public string amount1 = "0.01";
    public string amount2 = "0.02";
    public string textdata ; // from response
    // Start is called before the first frame update
    void Start()
    {
        userwallet = PlayerPrefs.GetString("PlayerWallet");
        // wallet address = playerprefs.getint("StoredWallet") // gotten from input field in the main menu ()
        string walletAddressToCheck = PlayerPrefs.GetString("PlayerWallet");//"your_wallet_address_here"; game wallet

        CheckLastCkBTCMovement(walletAddressToCheck); // instead of checking in start. check after application.focus is called

       // selectwallet();
      // Debug.Log(JSONReader.GetJSON(json).from_account_identifier);
    }
    public static class JSONReader
    {
        public static JSONExample GetJSON(TextAsset json)
        {
            JSONExample example = JsonUtility.FromJson<JSONExample>(json.text);
            return example;
        }
    }
    [System.Serializable]
    public class JSONExample
    {
        public string amount;
        public string from_account_identifier;
        public string to_account_identifier;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
  // private string icpChainUrl = "https://ledger-api.internetcomputer.org/transactions?limit=10";  // "https://icpchain.example.com";  / Replace with the actual ICP chain server URL

    public void CheckLastCkBTCMovement(string walletAddress)
    {
        StartCoroutine(GetLastCkBTCMovement(walletAddress));
    }

    private IEnumerator GetLastCkBTCMovement(string walletAddress)
    {
        string requestUrl = "https://ledger-api.internetcomputer.org/transactions?limit=10";// $"{icpChainUrl}/api/last_ckbtc_movement?wallet={walletAddress}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseJson = webRequest.downloadHandler.text;
                textdata = responseJson;

                Debug.Log("Response: " + responseJson);
                selectGamewalletAddress();


                // json = new TextAsset(responseJson);
                // Debug.Log(JSONReader.GetJSON(json).amount);
                // Parse the JSON response and extract the last ckBTC movement details
                // Example: string lastMovementAmount = ...;
                // Example: string receiverWallet = ...;
                // Use the extracted data as needed
                //  ParseJson(responseJson);

            }
        }
    }
    void getdetails(string responsejson)
    {
        string jsonString =responsejson;

        JSONExample[] transactions = JsonUtility.FromJson<JSONExample[]>(jsonString);

        foreach(JSONExample transaction in transactions)
        {
            Debug.Log("Sender:" + transaction.from_account_identifier);
            Debug.Log("Receiver:"+ transaction.to_account_identifier);
            Debug.Log("amount sent:" + transaction.amount);

            Debug.Log("-------------------");
        }
    }
    void selectGamewalletAddress()
    {
        string searchString = gamewallet;
        string selectedText = SelectText(textdata, searchString);
        if(selectedText!= null)
        {
            Debug.Log("Selected text :" + selectedText);
            // set transaction manager to true and 
        }
        else
        {
            Debug.Log("Error not found");
        }
    }
    void selectUserwalletAddress()
    {
        string searchString = userwallet;
        string selectedText = SelectText(textdata, searchString);
        if (selectedText != null)
        {
            Debug.Log("Selected text :" + selectedText);
            // set transaction manager to true and 
        }
        else
        {
            Debug.Log("Error not found");
        }
    }
    void selectAmount1()
    {
        string searchString = amount1;
        string selectedText = SelectText(textdata, searchString);
        if (selectedText != null)
        {
            Debug.Log("Selected text :" + selectedText);
            // set transaction manager to true and 
        }
        else
        {
            Debug.Log("Error not found");
        }
    }
    void selectAmount2()
    {
        string searchString = amount2;
        string selectedText = SelectText(textdata, searchString);
        if (selectedText != null)
        {
            Debug.Log("Selected text :" + selectedText);
            // set transaction manager to true and 
        }
        else
        {
            Debug.Log("Error not found");
        }
    }

    string SelectText(string text,string searchstring)
    {
        int index = text.IndexOf(searchstring);
        if (index >= 0)
        {
            return
                text.Substring(index, searchstring.Length);
        }
        else
        {
            return null;
        }
    }
  /* void ParseJson( string responseJson)
    {
       // CkBTCMovementData movementData = JsonConvert.DeserializeObject<CkBTCMovementData>(responseJson);
        CkBTCMovementData movementData = JsonUtility.FromJson<CkBTCMovementData>(responseJson);

        Debug.Log("Parsed wallet address: " + movementData.walletAddress);
        Debug.Log("Parsed amount: " + movementData.amount);
        Debug.Log("Parsed receiver wallet: " + movementData.receiverWallet);
    }*/

}
/*[System.Serializable]
public class CkBTCMovementData
{
    public string walletAddress;
    public float amount;
    public string receiverWallet;
}*/

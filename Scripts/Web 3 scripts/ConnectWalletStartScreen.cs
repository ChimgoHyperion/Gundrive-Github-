using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectWalletStartScreen : MonoBehaviour
{
    public Text walletaddressDisplayed;
    public InputField enterwalletaddress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        walletaddressDisplayed.text = PlayerPrefs.GetString("PlayerWallet");
    }

    public void savewalletaddress()
    {
        walletaddressDisplayed.text = enterwalletaddress.text;
        PlayerPrefs.SetString("PlayerWallet", enterwalletaddress.text);
    }
    public void ToggleStartScreen(GameObject connectedState, GameObject disconnectedState)
    {
        connectedState.SetActive(true);
        disconnectedState.SetActive(false);
    }
}

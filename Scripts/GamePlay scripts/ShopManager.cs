using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShopManager : MonoBehaviour
{
    public ButtonStates buttonStates;
    public Button skin2,skin3,skin4,skin5,skin6, skin7, skin8, skin9, skin10, skin11, skin12,skin13;
    public int TotalCoins;
    public Button  buy2, buy3, buy4, buy5, buy6,buy7,buy8,buy9,buy10,buy11,buy12,buy13;
  //  public Text coins;
    private string ButtonStatesPath;
    // sold animation
    public GameObject SoldText;
    // Start is called before the first frame update
    void Start()
    {
     
        ButtonStatesPath = $"{Application.persistentDataPath}/ButtonStates.json";
        if (File.Exists(ButtonStatesPath))
        {
            string json = File.ReadAllText(ButtonStatesPath);
            buttonStates = JsonUtility.FromJson<ButtonStates>(json);

           // string json = JsonUtility.ToJson(buttonStates);
           // File.WriteAllText(ButtonStatesPath, json);
        }
        TotalCoins = PlayerPrefs.GetInt("Coins");
        RerenderShop();
    }
    // Update is called once per frame
    void Update()
    {
        TotalCoins = PlayerPrefs.GetInt("Coins");
     
    }
    IEnumerator ActivateSoldText()
    {
        SoldText.SetActive(true);
        yield return new WaitForSeconds(1f);
        SoldText.SetActive(false);
    }

    public void Reset()
    {
        File.Delete(ButtonStatesPath);
    }
    public void BuySkin2( int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            buttonStates.skin2Unlocked = true;
            StartCoroutine(ActivateSoldText());
            RerenderShop();
            SaveJson();
            
        }
    }
    public void BuySkin3( int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin3Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin4(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin4Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin5(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin5Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin6( int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin6Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin7(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin7Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin8(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin8Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin9(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin9Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin10(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin10Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin11(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin11Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin12(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin12Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
    public void BuySkin13(int price)
    {
        if (TotalCoins >= price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins - price);
            StartCoroutine(ActivateSoldText());
            buttonStates.skin13Unlocked = true;
            RerenderShop();
            SaveJson();
        }
    }
   
    public void RerenderShop()
    {
        if (buttonStates.skin2Unlocked)
        {
            skin2.interactable = true;
            buy2.gameObject.SetActive(false);

        }
        if (buttonStates.skin3Unlocked)
        {
            skin3.interactable = true;
            buy3.gameObject.SetActive(false);
        }
        if (buttonStates.skin4Unlocked)
        {
            skin4.interactable = true;
            buy4.gameObject.SetActive(false);
        }
        if (buttonStates.skin5Unlocked)
        {
            skin5.interactable = true;
            buy5.gameObject.SetActive(false);
        }
        if (buttonStates.skin6Unlocked)
        {
            skin6.interactable = true;
            buy6.gameObject.SetActive(false);
        }
        if (buttonStates.skin7Unlocked)
        {
            skin7.interactable = true;
            buy7.gameObject.SetActive(false);
        }
        if (buttonStates.skin8Unlocked)
        {
            skin8.interactable = true;
            buy8.gameObject.SetActive(false);
        }
        if (buttonStates.skin9Unlocked)
        {
            skin9.interactable = true;
            buy9.gameObject.SetActive(false);
        }
        if (buttonStates.skin10Unlocked)
        {
            skin10.interactable = true;
            buy10.gameObject.SetActive(false);
        }
        if (buttonStates.skin11Unlocked)
        {
            skin11.interactable = true;
            buy11.gameObject.SetActive(false);
        }
        if (buttonStates.skin12Unlocked)
        {
            skin12.interactable = true;
            buy12.gameObject.SetActive(false);
        }
        if (buttonStates.skin13Unlocked)
        {
            skin13.interactable = true;
            buy13.gameObject.SetActive(false);
        }
       
    }
    private void SaveJson()
    {
        string json = JsonUtility.ToJson(buttonStates);
        File.WriteAllText(ButtonStatesPath, json);
    }

   
}

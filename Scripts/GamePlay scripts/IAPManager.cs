using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;


public class IAPManager : MonoBehaviour
{
    private string coin500 = "com.ectowaregames.gundrive.coin500";
    private string coin1500 = "com.ectowaregames.gundrive.coin1500";
    private string threeLives = "com.ectowaregames.gundrive.3lives";
    private string fake500 = "com.ectowaregames.gundrive.test500";
    int currentCoins;
    int currentLives;
    public GameObject reasonTextGameObject;
    public Text reason;
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == coin500)
        {
            // reward player
            currentCoins= PlayerPrefs.GetInt("Coins");
            currentCoins += 500;
            PlayerPrefs.SetInt("Coins", currentCoins);
            Debug.Log("You gained 500 coins");
        }
        if (product.definition.id == fake500)
        {
            // reward player test iap button
            currentCoins = PlayerPrefs.GetInt("Coins");
            currentCoins += 500;
            PlayerPrefs.SetInt("Coins", currentCoins);
            Debug.Log("You gained 500 coins");
        }
        if (product.definition.id == coin1500)
        {
            // reward player
            currentCoins = PlayerPrefs.GetInt("Coins");
            currentCoins += 1500;
            PlayerPrefs.SetInt("Coins", currentCoins);
            Debug.Log("You gained 1500 coins");
        }
        if (product.definition.id == threeLives)
        {
            currentLives = PlayerPrefs.GetInt("ExtraLivesNumber");
            currentLives += 3;
            PlayerPrefs.SetInt("ExtraLivesNumber", currentLives);
            Debug.Log("You Gained 3 Lives");
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        reasonTextGameObject.SetActive(true);
        reason.text = product.definition.id + "failed because" + failureReason;
        Debug.Log(product.definition.id + "failed because" + failureReason);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        reasonTextGameObject.SetActive(false);
    }
}

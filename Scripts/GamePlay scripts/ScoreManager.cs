using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int Highscore =0; // will be used in stats scene
  //  public Text HighscoreText;
    public Text ScoreText;
    public Text coinsCollectedText;
    public int Score = 0;
    public int CoinsCollected=0;
    public int coinsStored; // will be used in store scene
    int enemiesKilled;
    int totalEnemiesKilled;

    // death panel stats
    public Text deathPanelScore;
    public Text deathpanelHighscore;
    public Text deathpanelcoinsCollected;
    public Text deathpaneltotalCoins;
    int currentCoins;

    // HighScore and Rank Increase indicators
    public GameObject HighscoreIndicator;
    public GameObject NewRankIndicator;


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinsStored = PlayerPrefs.GetInt("Coins");
       // coinsStored = PlayerPrefs.GetInt("dinai",)
        Highscore = PlayerPrefs.GetInt("Highscore", 0);
        ScoreText.text = "Score :" + Score.ToString();
        coinsCollectedText.text =  CoinsCollected.ToString();
        totalEnemiesKilled = PlayerPrefs.GetInt("EnemiesKilled");

       
    }
    public void AddPoints()
    {
        Score += 10 ;
        enemiesKilled++;
      
      
        ScoreText.text = "Score :" + Score.ToString();
        if (Highscore < Score)
        {
            // update highscore
            PlayerPrefs.SetInt("Highscore", Score);
          //  HighscoreIndicator.SetActive(true);
          //  StartCoroutine(Disable());
        }

    }
    public void AddCoins( int addition)
    {
        CoinsCollected += addition;
        coinsCollectedText.text =  CoinsCollected.ToString();
    }
    public void rewardplayer()
    {
        //  CoinsCollected += 50;
        //  PlayerPrefs.SetInt("Coins", coinsStored + 50);
        // deathpaneltotalCoins.text = PlayerPrefs.GetInt("Coins").ToString();

       
        currentCoins = PlayerPrefs.GetInt("Coins");
        currentCoins += 50;
        PlayerPrefs.SetInt("Coins", currentCoins);
        Debug.Log("You gained 50 coins");
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        PlayerPrefs.DeleteKey("Coins");
    }
    public void SaveCoins()
    {
        PlayerPrefs.SetInt("EnemiesKilled", totalEnemiesKilled + enemiesKilled);
        if (Highscore < Score)
        {
            // update highscore
           HighscoreIndicator.SetActive(true);
            StartCoroutine(Disable());
            Highscore = Score;

            

            if (Highscore > 500 && Highscore <= 1200)
            {
                NewRankIndicator.SetActive(true);
                StartCoroutine(Disable());
            }
            
            if (Highscore > 1200 && Highscore <= 2400)
            {
                NewRankIndicator.SetActive(true);
                StartCoroutine(Disable());
            }
        
            if (Highscore > 2400)
            {
                NewRankIndicator.SetActive(true);
                StartCoroutine(Disable());
            }
            deathpanelHighscore.text = "Higscore:" + Highscore;
           
        }
        currentCoins = PlayerPrefs.GetInt("Coins");
        currentCoins += CoinsCollected;
        PlayerPrefs.SetInt("Coins", currentCoins);

        //  PlayerPrefs.SetInt("Coins", coinsStored + CoinsCollected);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
       // coinsStored = PlayerPrefs.GetInt("Coins");


        deathpanelcoinsCollected.text = "Earned:" + CoinsCollected.ToString();
        deathpanelHighscore.text = "Highscore:" + Highscore.ToString();
        deathPanelScore.text = ScoreText.text;
        deathpaneltotalCoins.text = "Total:" + PlayerPrefs.GetInt("Coins");
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(9f);
        HighscoreIndicator.SetActive(false);
        NewRankIndicator.SetActive(false);
    }
}

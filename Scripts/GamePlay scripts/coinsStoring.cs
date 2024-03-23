using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinsStoring : MonoBehaviour
{
    public static coinsStoring instance;
    public int CoinsStored;
    public Text coinsStoredText;
    public Text enemiesKilledText;
    public int HighScore;
    [SerializeField] Text highScoreText;
    [SerializeField] Text highscoreMainMenu;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CoinsStored = PlayerPrefs.GetInt("Coins");
        coinsStoredText.text = ":"+ CoinsStored.ToString();
        enemiesKilledText.text = "Droids Killed :" + PlayerPrefs.GetInt("EnemiesKilled");
        HighScore = PlayerPrefs.GetInt("Highscore");
        highScoreText.text = "HighScore :" + HighScore.ToString();
        highscoreMainMenu.text = "HighScore :" + HighScore.ToString();

    }
}

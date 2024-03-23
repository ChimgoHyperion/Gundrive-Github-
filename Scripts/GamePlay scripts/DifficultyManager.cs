using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public GameObject Deathpanel,LeaderBoardPanel;

     MovementandShooting Player;
    EnemyManager enemyManager;
    public PlayfabManager playfabManager;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        enemyManager.ReturnHeealth();
        
       
    }
    public void EndGame()
    {
        playfabManager.SendLeaderboard(PlayerPrefs.GetInt("Highscore"));
        ScoreManager.instance.SaveCoins(); // this method also has the code that send the score to the server leaderBoard
        StartCoroutine(wait());
    }
   IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        LeaderBoardPanel.SetActive(true);
        playfabManager.SendLeaderboard(PlayerPrefs.GetInt("Highscore"));
        yield return new WaitForSeconds(4f);
        LeaderBoardPanel.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        Deathpanel.SetActive(true);
        yield break;
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<MovementandShooting>();
        enemyManager = FindObjectOfType<EnemyManager>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Player.health <= 0)
        {
            EndGame();
            ScoreManager.instance.SaveCoins();
        }*/
    }
    

}

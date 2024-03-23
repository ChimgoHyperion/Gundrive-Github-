using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreBoard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;

    Dictionary<Player, ScoreBoardItem> scoreboardItems = new Dictionary<Player, ScoreBoardItem>();
    public GameObject winnerPanel;
    public Text WinnerText;

    
    // Start is called before the first frame update
    void Start()
    {
        foreach( Player player in PhotonNetwork.PlayerList)
        { 
            AddScoreBoardItem(player);
        }
    }
    void Update()
    {
        /*  foreach(Player player in PhotonNetwork.PlayerList)
          {
              if (player.CustomProperties.TryGetValue("Deaths", out object deaths))
              {
                  int lowestdeaths = int.MaxValue;

                  int currentvalue = (int)deaths;
                  if (currentvalue < lowestdeaths)
                  {
                      lowestdeaths = currentvalue;
                  }
                  WinnerText.text = lowestdeaths.ToString();
              }
          }

          int leastDeaths = int.MaxValue;
          Player winner = null;
          foreach (Player player in PhotonNetwork.PlayerList)
          {
              int deaths = (int)player.CustomProperties["Deaths"];

              if (deaths < leastDeaths)
              {
                  leastDeaths = deaths;
                  winner = player;
              }
          }
          WinnerText.text = "THE WINNER" + winner.NickName + ":" + leastDeaths + "deaths.";*/

       /* foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.TryGetValue("Kills", out object kills))
            {
                int higestKills =0;

                int currentvalue = (int)kills;
                if (currentvalue > higestKills)
                {
                    higestKills = currentvalue;
                }
                WinnerText.text = higestKills.ToString();
            }
        }
        
        int mostKills = 0;
        Player winner = null;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            int kills = (int)player.CustomProperties["Kills"];

            if (kills > mostKills)
            {
                mostKills = kills;
                winner = player;
            }
        } */
      //  WinnerText.text = "THE WINNER :" + winner.NickName + " :" + mostKills + " kills."; // removed because of change in timer script
    }

    public void DisplayWinner()
    {
        winnerPanel.SetActive(true);


        int leastDeaths = int.MaxValue;
        Player winner = null;
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            int deaths = (int)player.CustomProperties["Deaths"];

            if (deaths < leastDeaths)
            {
                leastDeaths = deaths;
                winner = player;
            }
        }
        WinnerText.text = "THE WINNER :" + winner.NickName + ":" + leastDeaths + "deaths.";
    }
    public void DisplayWinnerByKills()
    {
        winnerPanel.SetActive(true);


        int mostKills = 0;
        Player winner = null;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            int kills = (int)player.CustomProperties["Kills"];

            if (kills > mostKills)
            {
                mostKills = kills;
                winner = player;
            }
        }
        WinnerText.text = "THE WINNER :" + winner.NickName + ":" + mostKills + "kills.";
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreBoardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
       
    }
    void AddScoreBoardItem(Player player)
    {
        ScoreBoardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreBoardItem>();
        item.Initialize(player);
        scoreboardItems[player] = item;
    }
    void RemoveScoreboardItem(Player player)
    {
        Destroy(scoreboardItems[player].gameObject);
        scoreboardItems.Remove(player);
    }
    // Update is called once per frame
   

}

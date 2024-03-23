using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreBoardItem : MonoBehaviourPunCallbacks
{
    public Text userName;
    public Text deathcount;
    public Text KillCount; // new scection created for number of kills follow chatgpt procedure
    public Text Points; // kills plus deaths to give overall sccore will test in update
    public int PointsCount;

    Player player;
    public int deathsNumber;
    public  int KillsNumber;
    public void Initialize( Player  Player ) // find where this function should be called
    {
        userName.text = Player.NickName;
       // player = PhotonNetwork.LocalPlayer;
        this.player = Player;
        updateStats();
    }
    void updateStats()
    {

       /*int totaldeaths = (int)PhotonNetwork.LocalPlayer.CustomProperties["Deaths"];
        deathcount.text = totaldeaths.ToString();*/


        if(player.CustomProperties.TryGetValue("Deaths",out object deaths))
        {
            deathsNumber = (int)deaths;
            deathcount.text = deaths.ToString();
        }
       

      //  RPC_PointEvaluate(kills., deaths);
    }
    void UpdateKills()
    {
        if (player.CustomProperties.TryGetValue("Kills", out object kills))
        {
            KillsNumber = (int)kills;
            KillCount.text = kills.ToString();
        }
    }

    [PunRPC]
    void RPC_PointEvaluate(int kills,int deaths)
    {
        PointsCount = kills + deaths;
        Hashtable hashPoints = new Hashtable();
        hashPoints.Add("Points", Points);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashPoints);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            if (changedProps.ContainsKey("Deaths"))
            {
                // deathcounter should be in player manager and is stored in a hashtable
                updateStats();
            }
            if (changedProps.ContainsKey("Kills"))
            {
                // deathcounter should be in player manager and is stored in a hashtable
                UpdateKills();
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       /* int totaldeaths = (int)PhotonNetwork.LocalPlayer.CustomProperties["Deaths"];
        deathcount.text = totaldeaths.ToString(); */  // put in update just in case

        if (player.CustomProperties.TryGetValue("Deaths", out object deaths))
        {
            deathsNumber = (int)deaths;
            deathcount.text = deaths.ToString();
        }
        if (player.CustomProperties.TryGetValue("Kills", out object kills))
        {
            KillsNumber = (int)kills;
            KillCount.text = kills.ToString();
        }
        
    }
}

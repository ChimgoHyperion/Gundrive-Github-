using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class buttonSoundHolder : MonoBehaviour
{

    public AudioClip ShieldClip, TeleportClip, HealthClip, BoostClip, collectionClip, polycoinClip, buttonClick, enemydeathClip
        , PlayerDeathClip;
    public AudioSource source;
    public AudioSource source2, source3;
    public bool isrevealed = false;
    public GameObject leaderBoard;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void QuitMultiplayer()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }
    public void Shield()
    {
        source.PlayOneShot(ShieldClip);
       // but has to be edited for multiplayer probably a new Script
    }
    public void Health()
    {
        source.PlayOneShot(HealthClip);
    }
    public void teleport()
    {
        source.PlayOneShot(TeleportClip);
    }
    public void Boost()
    {
        source.PlayOneShot(BoostClip) ;
    }
    public void collection()
    {
        source.PlayOneShot(collectionClip);
    }
    public void CoinSound()
    {
        source.PlayOneShot(polycoinClip);
    }
    public void ButtonClick()
    {
        source2.PlayOneShot(buttonClick);
    }
    public void EnemyDeath()
    {
        source2.PlayOneShot(enemydeathClip);
    }
    public void PlayerDeath()
    {
        source3.volume = 0.2f;
        source3.PlayOneShot(PlayerDeathClip);
       
    }

    public void ShowLeaderBoard()
    {
        isrevealed = !isrevealed;
        if(isrevealed== true)
        {
            leaderBoard.SetActive(true);
        }
        else
        {
            leaderBoard.SetActive(false);
        }
    }
    [PunRPC] void PlaySound( AudioClip clip)
    {
        source2.PlayOneShot(clip);
    }
    public void PlayfromSource( AudioClip clip)
    {
        PlaySound(clip);
     //   this.GetComponent<PhotonView>().RPC("PlaySound", RpcTarget.AllBuffered,clip);
    }
    public void ShieldMulti()
    {
        PlayfromSource(ShieldClip);
    }
    public void HealthMulti()
    {
        PlayfromSource(HealthClip);
    }
    public void teleportMulti()
    {
        PlayfromSource(TeleportClip);
    }
    public void BoostMulti()
    {
        PlayfromSource(BoostClip);
    }
    public void collectionMulti()
    {
        PlayfromSource(collectionClip);
    }
    public void CoinSoundMulti()
    {
        PlayfromSource(polycoinClip);
    }
    public void ButtonClickMulti()
    {
        PlayfromSource(buttonClick);
    }
    public void EnemyDeathMulti()
    {
        PlayfromSource(enemydeathClip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

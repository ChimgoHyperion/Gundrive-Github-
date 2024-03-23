using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class TimerScript : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public int Duration;

    private int remainingDuration;

    private bool Pause;

    public GameObject ScoreBoard;
    public ScoreBoard board;
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        Being(Duration);
    }
    public void OnPointerClick(PointerEventData eventdata)
    {
        Pause = !Pause;
    }

    private void Being(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{ remainingDuration / 60:00}: {remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        print("End");
        ScoreBoard.SetActive(true);
     //   board.DisplayWinnerByKills();
        StartCoroutine(WaitBeforeLoad());
    }
    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(10f);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
        // attempting the use of code to remove all players from the game
        foreach (Player player in PhotonNetwork.PlayerList)
        {
           // PhotonNetwork.Disconnect();
           
        }
       // ClearGameIfMaster();
       
    }

    void ClearGameIfMaster()
    {
        if (PhotonNetwork.IsMasterClient) // apparently doesnt work. just removes onlly the master client from the game room
        {
            view.RPC(nameof(ExitGameForAll), RpcTarget.All);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("Lobby");
        }
    }

    [PunRPC]
    private void ExitGameForAll()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

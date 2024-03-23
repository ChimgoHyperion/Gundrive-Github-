using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviourPun,IPunObservable
{
     MultiplayerMoveAndShoot player;
     Vector3 remoteplayerposition;
    
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MultiplayerMoveAndShoot>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            return;
        }
        var LagDistance = remoteplayerposition - transform.position;
        if (LagDistance.magnitude > 5f)
        {
            transform.position = remoteplayerposition;
            LagDistance = Vector3.zero;
        }
        if (LagDistance.magnitude > 0.11f)
        {
            // player is nearly at the point
           
            player.direction.x = 0;
            player.direction.y = 0;
        }
        else
        {
            // player has to go to the point
            player.direction.x = LagDistance.normalized.x;
            player.direction.y = LagDistance.normalized.y;

        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            remoteplayerposition = (Vector2)stream.ReceiveNext();
        }
    }
}

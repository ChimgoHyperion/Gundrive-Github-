using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PickUpMulti : MonoBehaviour
{

    private Inventory inventory;
    public GameObject Weapon;
  //  private Transform Guncontainer;
    public Sprite newImage;
    public float deathTime;
    public Button button;
    buttonSoundHolder soundHolder;
    PhotonView view;



    void Start()
    {
        soundHolder = FindObjectOfType<buttonSoundHolder>();
        view = GetComponent<PhotonView>();
        GetComponent<PhotonView>().TransferOwnership(view.Owner);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "World")
        {
            GetComponent<PhotonView>().RPC(nameof(MakeStatic), RpcTarget.AllBuffered);
        }

        if (collision.gameObject.tag == "Player")
        {
            GetComponent<PhotonView>().RPC(nameof(MakeStatic), RpcTarget.AllBuffered);
        }
       
    }
    [PunRPC] void MakeStatic()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // must this Be RPCed???
    }
   

   
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitDestroy());
        GetComponent<PhotonView>().TransferOwnership(view.Owner);
    }
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(deathTime);
        GetComponent<PhotonView>().RPC(nameof(Destroy), RpcTarget.AllBuffered);
    }
    [PunRPC]
    void Destroy()
    {
        Destroy(gameObject);
    }
}

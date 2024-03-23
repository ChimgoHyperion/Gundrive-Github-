using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class blackHolePickup : MonoBehaviour
{
   
  
    public float deathTime;
    public Button button;
   
    PhotonView view;



    void Start()
    {
       
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
    [PunRPC]
    void MakeStatic()
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




    /*  private Inventory inventory;
      //  public Image BlackHoleButton;
      //  public Image BombContainer;
      buttonSoundHolder soundHolder;
      // Start is called before the first frame update
      void Start()
      {
          inventory = GameObject.FindGameObjectWithTag("GunContainer").GetComponent<Inventory>();
          //   ConsumableButton.transform.localScale = new Vector3(1, 1, 1);
          soundHolder = FindObjectOfType<buttonSoundHolder>();
          //  BlackHoleButton = GameObject.Find("BlackHoleButton").GetComponent<Image>();

      }
      private void OnCollisionEnter2D(Collision2D collision)
      {
          if (collision.gameObject.tag == "Player")
          {
              /*  if(BlackHoleButton.gameObject.activeSelf== false)
                {
                    BlackHoleButton.gameObject.SetActive(true);
                }*/
    /*  if (inventory.bombsCollected < 4)
      {
          inventory.bombsCollected++;
          soundHolder.collection();
          Destroy(gameObject);
      }*/
    /*  for (int i = 0; i < inventory.bombsCollected; i++)
      {

          if (inventory.bombSlotfull[i] == false)
          {
              inventory.bombSlotfull[i] = true;
              soundHolder.collection();

              // ConsumableButton.rectTransform.anchoredPosition = inventory.consumableSlots[i].rectTransform.anchoredPosition;

              Destroy(gameObject);
              break;
          }
      } **
}
}
// Update is called once per frame
void Update()
{
 Destroy(gameObject, 10f);
} */
}

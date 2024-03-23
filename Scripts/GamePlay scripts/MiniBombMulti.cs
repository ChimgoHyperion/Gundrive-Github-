using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class MiniBombMulti : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject miniBomb;
    public float range = 15f;

    
    private float timebtwShots;
    public float starttimebtwShots;
    public float bulletsLeft;
 
    public AudioClip shootingClip;
    public WeaponHolderMulti multi;
    public float startBullets;
    PhotonView view;
    public PhotonView playerview;

    public int myshooterId;
    
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        myshooterId = playerview.ViewID;
    }

    // Update is called once per frame
    void Update()
    {
        myshooterId = playerview.ViewID;
        if (bulletsLeft <= 0)
        {
            gameObject.SetActive(false);
            multi.hasWeapon = false;
            bulletsLeft = startBullets;
            view.RPC(nameof(Deactivate), RpcTarget.AllBuffered); 
        }
        if(this.gameObject.activeSelf== false)
        {
            view.RPC(nameof(Deactivate2), RpcTarget.AllBuffered);
        }
    }

   
    public void LaunchMulti()
    {
        
        GetComponent<PhotonView>().RPC("Launch_Multi", RpcTarget.AllBuffered);

    }
    [PunRPC]
    public void Launch_Multi()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (timebtwShots <= 0)
            {
                if (bulletsLeft > 0)
                {
                   
                    GameObject bombInstance = PhotonNetwork.Instantiate(miniBomb.name, spawnPoint.position, Quaternion.identity); // dunno what the 0 is doing yet 

                    bombInstance.GetComponent<miniCluster>().shooterId = myshooterId;

                  /*  PhotonView bombview = miniBomb.GetComponent<PhotonView>();

                    bombview.RPC(nameof(setBulletId), RpcTarget.AllBuffered, shooterId);
                    */
                    bombInstance.GetComponent<Rigidbody2D>().velocity = transform.right * range;

                    AudioMana.instance.PlaySound(shootingClip);
                }


                bulletsLeft--;
                timebtwShots = starttimebtwShots;
            }
            else
            {
                timebtwShots -= Time.deltaTime;
            }
        }
       


    }
    [PunRPC] public void setBulletId(int shooterId)
    {
        myshooterId = shooterId;
    }
    [PunRPC] void Deactivate()
    {
        gameObject.SetActive(false);
        multi.hasWeapon = false;
        bulletsLeft = startBullets;
    }
    [PunRPC]
    void Deactivate2()
    {
        gameObject.SetActive(false);
      //  multi.hasWeapon = false;
        bulletsLeft = startBullets;
    }
}

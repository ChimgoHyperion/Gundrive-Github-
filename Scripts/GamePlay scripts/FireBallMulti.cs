using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class FireBallMulti : MonoBehaviour
{
    public Transform shootingTip;


    // for shooting
    public GameObject bulletPrefab;

    private float timebtwShots;
    public float starttimebtwShots;
    public float Bulletspeed;
    // public Slider ammoBar;
    public int bulletsLeft;

    public float offset;

    public GameObject muzzleflash;

    // public Animator gunAnimator;
    public float intensity;
    public float time;
   

    // audio
    public AudioClip shootingClip;
    // multiplayer
    PhotonView view;
    public PhotonView playerview;
    private int myshooterId;
    public WeaponHolderMulti multi;
    public int startBullets;
    public buttonSoundHolder soundHolder;
    // Start is called before the first frame update
    // reloading
   
    void Start()
    {
        view = GetComponent<PhotonView>();
        shootingTip.rotation = transform.rotation;
        /* joystick = GameObject.FindGameObjectWithTag("WeaponStick").GetComponent<FixedJoystick>();
         ammoBar = GameObject.FindGameObjectWithTag("AmmoBar").GetComponent<Slider>();
         ammoBar.maxValue = bulletsLeft;*/
        myshooterId = playerview.ViewID;
       
    }
    public void Shoot()
    {

        view.RPC(nameof(ShootMulti), RpcTarget.AllBuffered);
    }
    [PunRPC] void ShootMulti()
    {
        
        if (bulletsLeft > 0)
        {
            if (timebtwShots <= 0)
            {
                GameObject bulletInstance = PhotonNetwork.Instantiate(bulletPrefab.name, shootingTip.position, Quaternion.identity);
                bulletInstance.GetComponent<fireballprojectile>().shooterId = playerview.ViewID;
                bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * Bulletspeed;

                soundHolder.PlayfromSource(shootingClip);
                // shake camera
                ScreenShake.instance.shakeCamera(intensity, time);
                // play recoil animation
                /* gunAnimator.SetTrigger("Shoot");*/
                Instantiate(muzzleflash, shootingTip.position, Quaternion.identity);
              
                bulletsLeft--;
                timebtwShots = starttimebtwShots;

            }
            else
            {
                timebtwShots -= Time.deltaTime;
            }
        }

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
        if (this.gameObject.activeSelf == false)
        {
            view.RPC(nameof(Deactivate2), RpcTarget.AllBuffered);
        }
       

    }
   
    [PunRPC]
    void Deactivate()
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

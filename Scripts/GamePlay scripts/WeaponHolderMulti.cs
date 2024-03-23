using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WeaponHolderMulti : MonoBehaviour
{
    // note that guncontainer does not need transform view at this point since its unstable. the gun children all need transform views
    // but the transform view is transform view(new) with use local turned Off . do not use the transform view classic for them
    int totalWeapons = 0;
    public GameObject[] guns;
    public GameObject Shield;
    GameObject weaponHolder;

    private bool buttonClicked = false;
    DestroyPowerUP destroyPowerUp;
    GameObject Gun;
    // gun control 
    public Joystick WeaponStick;
    bool gun1Selected;
    bool gun2Selected;

    Inventory inventory;
    public Sprite newImage;
    buttonSoundHolder soundHolder;

    public Slider ammoBar;
    public GameObject selesctedWeapon;
    public bool hasWeapon = false;
    PhotonView view;

    [SerializeField] float miniBombsbulletsLeft, fireballBulletsleft, BulletsgunLeft;
    public GameObject particleOBJ;
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        weaponHolder = this.gameObject;
        destroyPowerUp = FindObjectOfType<DestroyPowerUP>();
      //  PhotonNetwork.Instantiate(particleOBJ.name,this.transform.localPosition, Quaternion.identity);
        inventory = GetComponent<Inventory>();
        soundHolder = GameObject.FindObjectOfType<buttonSoundHolder>();

        ammoBar.maxValue = 100;
        view = GetComponent<PhotonView>();
    }


    public void selectfirst()
    {
        guns[0].SetActive(true);
        guns[1].SetActive(false);

    }
    public void selectSecond()
    {

        guns[0].SetActive(false);
        guns[1].SetActive(true);

    }



    // Update is called once per frame
    void Update()
    {
        
        
         /*   totalWeapons = weaponHolder.transform.childCount;
            guns = new GameObject[totalWeapons];


            for (int i = 0; i < totalWeapons; i++)
            {
                guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            }

            */

            foreach (Transform child in transform)
            {
               if(child.gameObject.activeSelf== true)
               {
                if (Mathf.Abs(WeaponStick.Horizontal) > 0.5 || Mathf.Abs(WeaponStick.Vertical) > 0.5)
                {
                    child.GetComponent<GeneralGun>().checkGun();
                    child.GetComponent<GeneralGun>().willShoot = true;
                }
                else
                {
                    child.GetComponent<GeneralGun>().willShoot = false;
                }



                if (child != null)
                {
                    ammoBar.maxValue = child.GetComponent<GeneralGun>().maxAmmo;
                    ammoBar.value = ammoBar.maxValue;
                    ammoBar.value = child.GetComponent<GeneralGun>().currentBulletsLeft;
                }

               }



            //  ammoBar.value= child.GetComponent<GeneralGun>(). it refers to the particular script attached to the gun and gets the bulletsleft value;
            // also the ammoBar.max value is equal to the child's maxBullets
            }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*    GameObject gun = collision.gameObject.GetComponent<PickUpMulti>().Weapon;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {

                    inventory.isFull[i] = true;
                    soundHolder.collection();

                /*  Instantiate(gun, this.gameObject.transform, false);

                  GetComponent<PhotonView>().RPC("InstantiateGun", RpcTarget.AllBuffered, gun, collision);


                  inventory.slots[i].GetComponent<Image>().enabled = true;
                    guns[0].SetActive(true);
                    break;
                }
            }*/
        if (hasWeapon==false)
        {
            if (collision.gameObject.tag == "MiniBomb")
            {
                collision.gameObject.SetActive(false);
                view.RPC(nameof(ActivateMiniBomb), RpcTarget.All);
                guns[0].SetActive(true);
                hasWeapon = true;
                PhotonNetwork.Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Bullets")
            {
                collision.gameObject.SetActive(false);
                view.RPC(nameof(ActivateBullets), RpcTarget.All);
                guns[1].SetActive(true);
                hasWeapon = true;
                PhotonNetwork.Destroy(collision.gameObject);

            }
            if (collision.gameObject.tag == "FireBall")
            {
                collision.gameObject.SetActive(false);
                view.RPC(nameof(ActivateFireBall), RpcTarget.All);
                guns[2].SetActive(true);
                hasWeapon = true;
                PhotonNetwork.Destroy(collision.gameObject);

            }
            if (collision.gameObject.tag == "FireSpray")
            {
                collision.gameObject.SetActive(false);
                view.RPC(nameof(ActivateFireSpray), RpcTarget.All);
                guns[3].SetActive(true);
                hasWeapon = true;
                PhotonNetwork.Destroy(collision.gameObject);

            }
            if (collision.gameObject.tag == "IceSpray")
            {
                collision.gameObject.SetActive(false);
                view.RPC(nameof(ActivateIceSpray), RpcTarget.All);
                guns[4].SetActive(true);
                hasWeapon = true;
                PhotonNetwork.Destroy(collision.gameObject);

            }
            if (collision.gameObject.tag == "Laser")
            {
                collision.gameObject.SetActive(false);
                view.RPC(nameof(ActivateLaser), RpcTarget.All);
                guns[5].SetActive(true);
                hasWeapon = true;
                PhotonNetwork.Destroy(collision.gameObject);

            }
        }

        if (collision.gameObject.tag == "Shield")
        {
            collision.gameObject.SetActive(false);
            soundHolder.collection();
            view.RPC(nameof(ActivateShield), RpcTarget.AllBuffered);
            PhotonNetwork.Destroy(collision.gameObject); // we need to use RPC here
        }

        if (collision.gameObject.tag == "BlackHole")
        {
            collision.gameObject.SetActive(false);
            // view.RPC(nameof(ActivateLaser), RpcTarget.All);
            BlackHoleBtn.SetActive(true);
            SpawnPoint.gameObject.SetActive(true);
            PhotonNetwork.Destroy(collision.gameObject);

        }
    }
    public GameObject BlackHoleBtn;
    public GameObject BlackHole;
    public Transform SpawnPoint;
    public void SpawnBhole()
    {
        view.RPC(nameof(SpawnBlackHole), RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void SpawnBlackHole()
    {
        PhotonNetwork.Instantiate(BlackHole.name, SpawnPoint.position, Quaternion.identity);
        BlackHoleBtn.SetActive(false);
        SpawnPoint.gameObject.SetActive(false);
    }
    [PunRPC]
    public void ActivateShield()
    {
        Shield.SetActive(true);
        Shield.GetComponent<PlayerShield>().StartCountDown();

    }
    [PunRPC] void InstatiateGun( GameObject gun,Collision2D collision)
    {
        Instantiate(gun, this.gameObject.transform, false);
        PhotonNetwork.Instantiate(gun.name, gameObject.transform.position, gameObject.transform.rotation);
       
    }
    [PunRPC] void ActivateMiniBomb()
    {
        if (hasWeapon == false)
        {
            guns[0].SetActive(true);
            guns[0].GetComponent<GeneralGun>().currentBulletsLeft = miniBombsbulletsLeft;
            hasWeapon = true;
        }
       
       
    }
    [PunRPC]
    void ActivateBullets()
    {
        if (hasWeapon == false)
        {
            guns[1].SetActive(true);
            guns[1].GetComponent<GeneralGun>().currentBulletsLeft = BulletsgunLeft;
            hasWeapon = true;
        }
       

    }
    [PunRPC]
    void ActivateFireBall()
    {
        if (hasWeapon == false)
        {
            guns[2].SetActive(true);
            guns[2].GetComponent<GeneralGun>().currentBulletsLeft = fireballBulletsleft;
            hasWeapon = true;
        }
    }
    [PunRPC]
    void ActivateFireSpray()
    {
        guns[3].SetActive(true);
        guns[3].GetComponent<GeneralGun>().currentBulletsLeft = 900;
        hasWeapon = true;

    }
    [PunRPC]
    void ActivateIceSpray()
    {
        guns[4].SetActive(true);
        guns[4].GetComponent<GeneralGun>().currentBulletsLeft = 800;
        hasWeapon = true;

    }
    [PunRPC]
    void ActivateLaser()
    {
        guns[5].SetActive(true);
        guns[5].GetComponent<GeneralGun>().currentBulletsLeft = 500;
        hasWeapon = true;

    }
  
}

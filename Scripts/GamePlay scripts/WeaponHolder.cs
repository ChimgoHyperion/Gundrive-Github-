using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    int totalWeapons = 0;
    public GameObject[] guns;
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
    public Animator gunanimator;
    // Start is called before the first frame update
    void Start()
    {
        weaponHolder = this.gameObject;
        destroyPowerUp = FindObjectOfType<DestroyPowerUP>();

        inventory = GetComponent<Inventory>();
        soundHolder = GameObject.FindObjectOfType<buttonSoundHolder>();
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
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];


        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
        }


        /* if (Mathf.Abs(WeaponStick.Horizontal) > 0.5 || Mathf.Abs(WeaponStick.Vertical) > 0.5)
         {
             foreach (Transform child in transform)
             {
                 child.GetComponent<GeneralGun>().checkGun();
             }

         }*/
        if (Mathf.Abs(WeaponStick.Horizontal) > 0.5 || Mathf.Abs(WeaponStick.Vertical) > 0.5)
        {
          //  gunanimator.SetTrigger("Shoot");

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gun =  collision.gameObject.GetComponent<PickUpMulti>().Weapon;
        
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {

                inventory.isFull[i] = true;
                soundHolder.collection();
                // Instantiate(Weapon, container.transform,false);
                Instantiate(gun, this.gameObject.transform, false);
                Destroy(gun.gameObject);
                // Instantiate(button, inventory.slots[i].rectTransform, false);

                //  Weapon.transform.localPosition = new Vector3(1.5f, -0.15f, 0f);


                // inventory.slots[i].GetComponent<Image>().sprite = newImage;

                inventory.slots[i].GetComponent<Image>().enabled = true;
                
                break;
            }
        }
        Destroy(collision.gameObject);
        if (collision.gameObject.tag == "World")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        /*  if (collision.gameObject.tag == "Enemy")
          {
              GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

          }
          if (collision.gameObject.tag == "Enemy5")
          {
              GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

          }*/
    }


}

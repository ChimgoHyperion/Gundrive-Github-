using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject Weapon;
    private Transform Guncontainer;
    public Sprite newImage;
    public float deathTime;
    public Button  button;
    buttonSoundHolder soundHolder;




    void Start()
    {
        soundHolder = FindObjectOfType<buttonSoundHolder>();
        Guncontainer = GameObject.FindGameObjectWithTag("GunContainer").transform;
        inventory = Guncontainer.GetComponent<Inventory>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     //  GameObject container = collision.gameObject;
        if (collision.gameObject.tag == "Player")
        {
           
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i]== false)
                {
                  
                    inventory.isFull[i] = true;
                    soundHolder.collection();
                    // Instantiate(Weapon, container.transform,false);
                    Instantiate(Weapon, Guncontainer.transform, false);

                    // Instantiate(button, inventory.slots[i].rectTransform, false);

                    //  Weapon.transform.localPosition = new Vector3(1.5f, -0.15f, 0f);


                    inventory.slots[i].GetComponent<Image>().sprite = newImage;

                    inventory.slots[i].GetComponent<Image>().enabled = true;

                 

                    Destroy(gameObject);
                    break;
                }
            }
        }
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
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, deathTime);
    }
}

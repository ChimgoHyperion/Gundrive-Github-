using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricGunScript : MonoBehaviour
{


    public ParticleSystem electicEffect;
    public float bulletsleft;

    // weapon movement
    public Joystick joystick;
  
    Vector2 Rotation;
    private float rotation2;
    private float rotation3;
    public bool facingRight = true;
    public GameObject detectCollision;
    public Slider ammoBar;
   
    //  public AudioClip shootingClip;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("WeaponStick").GetComponent<FixedJoystick>();
        electicEffect.Stop();

        
        ammoBar = GameObject.FindGameObjectWithTag("AmmoBar").GetComponent<Slider>();
        ammoBar.maxValue = bulletsleft;
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(joystick.Horizontal) > 0.5 || Mathf.Abs(joystick.Vertical) > 0.5)
        {
            if (bulletsleft > 0)
            {

                //  AudioMana.instance.PlaySound(shootingClip); 
                // source.Play();

                detectCollision.SetActive(true);
                electicEffect.Play();

                
                bulletsleft--;
            }

        }
        else
        {

            detectCollision.SetActive(false);
            electicEffect.Stop();
           
        }
        ammoBar.value = bulletsleft;
        if (bulletsleft <= 0)
        {
            Destroy(gameObject);
            ammoBar.maxValue = 100;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

}

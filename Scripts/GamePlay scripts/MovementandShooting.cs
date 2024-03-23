using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class MovementandShooting : MonoBehaviour
{
    // for movement and rotation
    public float Speed;
    public float force;
    public FixedJoystick movementJoystick;
    Rigidbody2D rb;
    public  float LerpTime;
    public int health ;
    public Slider healthBar;
   // public Text healthText;
    public bool facingRight = true;


    // ground check
  [SerializeField]  private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;

    public float boostAmount;
    [SerializeField] float maxBoostValue;
    public float boostFactor;
    public Slider boostBar;
    public float gravityScale;
    public float fallGravity;

    public GameObject Shield;
    public Transform[] teleportPoints;
    


    DifficultyManager difficultyManager;
    public GameObject weapon;
    WeaponScript weaponScript;


    public Color currentColor;
    public GameObject dustEffect;
    public GameObject damageEffect;
    public GameObject deatheffect;
    public Material WhiteMaterial;
    public Material spritesDefault;

    private Animator anim;
    public AudioClip hitRock;
    public AudioSource source;

    buttonSoundHolder soundHolder;
    public TrailRenderer trail;

    public float rigidBodySpeed;
    Vector2 direction;

    public Animator CharacterAnimator;
    // multiplayer variables
    PhotonView view;

    public void SlowLyDamage(int damageFactor)
    {
        health -= damageFactor;
    }


    public void TakeDamage (int damage)
    {
        StartCoroutine(shineWhite());
        if (Shield.activeSelf== true)
        {
            health -= 0;
        }else if(!Shield.activeSelf )
        {
            health -= damage;
            Instantiate(damageEffect, transform.position, Quaternion.identity);
           int num =  Random.Range(0, 1);
            switch (num)
            {
                case 0:
                    anim.SetTrigger("Squashing");
                    break;
                case 1:

                    anim.SetTrigger("Stretching");
                    break;
            }
           
          

        }
        else
        {
           // anim.SetBool("isStretched", false);
        }
        healthBar.value = health;
        /*  if (health <= 0)
          {
              die();

          }*/


    }
    public void die()
    {
        soundHolder.PlayerDeath();
        Instantiate(deatheffect, transform.position, Quaternion.identity);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        difficultyManager.EndGame();
        gameObject.SetActive(false);
    }
    IEnumerator shineWhite()
    {
      //  gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.GetComponent<SpriteRenderer>().material = WhiteMaterial;
        yield return new WaitForSeconds(0.05f);
     //   gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        gameObject.GetComponent<SpriteRenderer>().material = spritesDefault;

    }
    public void IncreaseHealth()
    {
        if (health<=80)
        {
            health += 20;
        }
        if (health>80 && health<=90)
        {
            health += 10;
        }
        if (health > 90)
        {
            health = 100;
        }
       
    }
    public void IncreaseBoost()
    {
        boostAmount = maxBoostValue;
    }
    public void ActivateShield()
    {
        Shield.SetActive(true);
        Shield.GetComponent<ShieldScript>().StartCountDown();
       
    }
    public void TeleportPlayer()
    {
        int pointNumber = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[pointNumber].position;
    }
    public GameObject BlackHoleBtn;
    public GameObject BlackHole;
    public Transform SpawnPoint;

    public void SpawnBhole()
    {
        Instantiate(BlackHole, SpawnPoint.position, Quaternion.identity);
        BlackHoleBtn.SetActive(false);
        SpawnPoint.gameObject.SetActive(false);
    }
   
   

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = health;
        boostBar.maxValue = maxBoostValue;
        difficultyManager = FindObjectOfType<DifficultyManager>();
        weaponScript = FindObjectOfType<WeaponScript>();
        rb = GetComponent<Rigidbody2D>();
        boostAmount = maxBoostValue;
        RandomPosition();
        currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        soundHolder = FindObjectOfType<buttonSoundHolder>();
        anim = GetComponent<Animator>();
       // trail = GetComponent<TrailRenderer>(); for jetpack sake
        StartCoroutine(startTrail());

        view = GetComponent<PhotonView>();
    }
    public void RandomPosition()
    {
        int pointNumber = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[pointNumber].position;
    }
    IEnumerator startTrail()
    {
        trail.enabled= false;
        yield return new WaitForSeconds(4f);
        trail.enabled = true;
    }
    private void Update()
    {
        
        healthBar.value = health;
    }
    IEnumerator enableEndGame()
    {
        yield return new WaitForSeconds(2);
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
    // Update is called once per frame

       
    void FixedUpdate()
    {
        healthBar.value = health;
        if (health <= 0)
        {
            die();

        }
        // movement

        // rb.AddForce(Normaldirection * force * Time.deltaTime, ForceMode2D.Impulse);
        //  rb.velocity = direction * force;

        // grounding

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);
        
            if (isGrounded)
            {
                if (boostAmount < maxBoostValue)
                    boostAmount += Time.deltaTime * boostFactor;
                trail.emitting = false;
            }
            if (boostAmount == 0)
            {
                boostAmount = Time.deltaTime;
            }
            if (isGrounded == false)
            {
                //  direction.y -= boostFactor * Time.deltaTime;
                if (boostAmount >= 0)
                    boostAmount -= Time.deltaTime * boostFactor;
                trail.emitting = true;
            }

            if (boostAmount >= 0)
            {
                direction = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
                rb.gravityScale = gravityScale;

            }
            else
            {
                direction = new Vector2(movementJoystick.Horizontal, 0);
                rb.gravityScale = fallGravity;
            }
            rb.MovePosition((Vector2)transform.position + direction * Speed * Time.deltaTime);


        // CharacterAnimator.SetBool("IsFlying", !isGrounded); // that is, when the player is not grounded set flying animation
        //...to true and vice versa

        /*  if(movementJoystick.Horizontal!=0 && isGrounded == true)
          {
              CharacterAnimator.SetBool("IsWalking",true); // if the joystick moves horizontally and player is on the ground, then play
              // ... walking animation
          } */
          
        // When its not grounded, fly
        if (isGrounded== false)
        {
            CharacterAnimator.SetBool("IsFlying", true);
        }
        // when it is grounded
        // a. when it is grounded and not walking
        if(isGrounded== true)
        {
            CharacterAnimator.SetBool("IsFlying", false);
            CharacterAnimator.SetBool("IsWalking", false);
        }
        // b. when it is grounded and is walking
        if(isGrounded== true && movementJoystick.Horizontal != 0)
        {
            CharacterAnimator.SetBool("IsFlying", false);
            CharacterAnimator.SetBool("IsWalking", true);
        }


        /*  if (boostAmount < maxBoostValue)
         {
             boostAmount += Time.deltaTime * boostFactor;
             if (boostAmount >= 0)
             {
                 direction = new Vector2(movementJoystick.Horizontal, 0);
                 //  rb.MovePosition((Vector2)transform.position + Normaldirection * Speed * Time.deltaTime);
                 // rb.gravityScale = gravityScale;
             }
         }
         boostAmount -= Time.deltaTime * boostFactor;
         if (boostAmount <= 0)
         {
             trail.emitting = false;
             // rb.MovePosition((Vector2)transform.position + NoboostDirection * Speed * Time.deltaTime);
             direction = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
             //   rb.gravityScale = fallGravity; //Mathf.Lerp(rb.gravityScale, fallGravity, 0.1f);
             boostAmount = Time.deltaTime;

         }*/

        boostBar.value = boostAmount;
       
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* if(collision.gameObject.tag== "World")
          {
              Instantiate(dustEffect, transform.position, Quaternion.identity);
              source.PlayOneShot(hitRock);
          }
          if (collision.gameObject.tag == "Enemy")
          {
              Instantiate(dustEffect, transform.position, Quaternion.identity);
              source.PlayOneShot(hitRock);
          }
          if (collision.gameObject.tag == "Enemy5")
          {
              Instantiate(dustEffect, transform.position, Quaternion.identity);
              source.PlayOneShot(hitRock);
          }*/
        if (collision.gameObject.tag == "BlackHole")
        {
            collision.gameObject.SetActive(false);
            // view.RPC(nameof(ActivateLaser), RpcTarget.All);
            BlackHoleBtn.SetActive(true);
            SpawnPoint.gameObject.SetActive(true);
            Destroy(collision.gameObject);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BottomDeath")
        {
            die();
            Instantiate(dustEffect, transform.position, Quaternion.identity);
            source.PlayOneShot(hitRock);
        }
    }







}

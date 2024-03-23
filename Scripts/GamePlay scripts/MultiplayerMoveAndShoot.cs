using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


public class MultiplayerMoveAndShoot : MonoBehaviourPun,IPunObservable
{
    // for movement and rotation
    public float Speed;
    public float force;
    public FixedJoystick movementJoystick;
    Rigidbody2D rb;
    public float LerpTime;
    public int health;
    public float healthForLaser;
    public Slider healthBar;
    // public Text healthText;
    public bool facingRight = true;


    // ground check
    private bool isGrounded;
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
  //  public Transform[] teleportPoints;



  //  DifficultyManager difficultyManager;
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

    public buttonSoundHolder soundHolder;
    TrailRenderer trail;

    public float rigidBodySpeed;
    public Vector2 direction;

    // multiplayer variables
    PhotonView view;
    public Text PlayerName;
    public GameObject PlayerCamera;
    public GameObject CM;
    public GameObject personlCanvas;
    // respawn variables
    public GameObject playerCanvas;
    public PlayerMangerr playermanager;
    public GameObject PlayerlistButton;
    public Player PlMove;

    public int deaths=0;
    [SerializeField] int kills;
    public SpriteRenderer myRend;
    // shooter id
    public int currentshooterId;
   [SerializeField] GameObject[] spawnPoints;

    public float startTimer = 5f;
    public float timePassed;
    public bool shouldRespawn = false;
    public GameObject guncontainer;
    public GameObject RespawnPanel;
    public Text respawnText;
    PhotonView killerview;

    // connections
    private const string PlayerInactiveProperty = "IsPlayerInacative";
    public Text killedByText;
    public Player myPlayer;
    public bool canDie = true;
   
    private void Awake()
    {
      //  PhotonNetwork.SendRate = 30; // default value 20
      //  PhotonNetwork.SerializationRate = 10;// default value 10
    }
    void Start()
    {
        myPlayer = PhotonNetwork.LocalPlayer;
        timePassed = startTimer;
        healthBar.value = health;
        boostBar.maxValue = maxBoostValue;
        //  difficultyManager = FindObjectOfType<DifficultyManager>();
        weaponScript = FindObjectOfType<WeaponScript>();
        rb = GetComponent<Rigidbody2D>();
        boostAmount = maxBoostValue;
        // int pointNumber = Random.Range(0, teleportPoints.Length);
        //  transform.position = teleportPoints[pointNumber].position;
        currentColor = gameObject.GetComponent<SpriteRenderer>().color;
       
        anim = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
        StartCoroutine(startTrail());
         healthForLaser = (float)health;

        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            PlayerName.text = PhotonNetwork.NickName;
            PlayerCamera.SetActive(true);
            CM.SetActive(true);
            personlCanvas.SetActive(true);
            if (PhotonNetwork.IsMasterClient)
            {
              //  PlayerlistButton.SetActive(true);
            }
        }
        else
        {
            PlayerName.text = view.Owner.NickName;
            PlayerName.color = Color.red;
        }
         view.TransferOwnership(view.Owner); // TRANSFER OWNERSHIP to creator of the gameobject
          if (view.IsMine)
          {
            //  SpawnPlayers.instance.localPlayer = this.gameObject;
          }
          if (!view.IsMine)
          {
             this.GetComponent<MultiplayerMoveAndShoot>().enabled = false; // this is the right setting to use else player wont die for some reason . another
             Destroy(rb);
             Destroy(this.GetComponent<Rigidbody2D>());
            // solution might be to reove view.ismine check in the dead mehtod and put it in the takedamage method
          }
       
        if (!view.IsMine && GetComponent<MultiplayerMoveAndShoot>() != null)
        {
           // Destroy(GetComponent<MultiplayerMoveAndShoot>()); // this works for when i testeed the game without Guncontainer.2 now testing w gn
        }
      
    }
    // this method is called whenever the player temporarily leaves the game
    private void SetPlayerInacative()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { PlayerInactiveProperty, true } });
        PhotonNetwork.Disconnect();
    }
    public void RejoinGame()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
   /* public override void OnConnectedToMaster()
    {
      
    }*/

   
     
    public void setshooter(int shooterID)
    {
        currentshooterId = shooterID;
        view.RPC(nameof( RPCsetshooter), RpcTarget.AllBuffered,shooterID);
    }

    [PunRPC]  void RPCsetshooter(int shooter)
    {
        currentshooterId = shooter;
    }
    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            if (Shield.activeSelf == true)
            {
                health -= 0; // check shield tomorrow
            }
            else if (!Shield.activeSelf)
            {
                health -= damage;
                PhotonNetwork.Instantiate(damageEffect.name, transform.position, Quaternion.identity);
            }
            
            if (view.IsMine)
            {
                // StartCoroutine(shineWhite());

            }

        }

    }
    public void SlowLyDamage(int damageFactor)
    {
        if (Shield.activeSelf == true)
        {
            healthForLaser -= 0; // check shield tomorrow
        }
        else if (!Shield.activeSelf)
        {
            healthForLaser -= damageFactor * Time.deltaTime;
            int newHealth = (int)healthForLaser;
            health = newHealth;
          //  healthBar.value = healthForLaser;
            PhotonNetwork.Instantiate(damageEffect.name, transform.position, Quaternion.identity);
        }
       
    }


    [PunRPC]
    private void Dead()
    {
      
       

        
        if (view.IsMine)
        {
            
            // handles death of the owner of this client
           
            soundHolder.EnemyDeath();
            PhotonNetwork.Instantiate(deatheffect.name, this.transform.position, Quaternion.identity);
            
            deaths++;
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { {"Deaths", deaths } });
          

            view.RPC(nameof(Disappear), RpcTarget.AllBuffered);
            RespawnPanel.SetActive(true);
            guncontainer.SetActive(false);
            trail.enabled = false;
            shouldRespawn = true;

            killerview = PhotonView.Find(currentshooterId);
            killerview.GetComponent<KillCounter>().AddKills();
           
        }
       
        // possible solution is to use another script
        /*  if (currentshooterId == 0)
          {
              return;
          }*/

        /* int playerKills=*/
        //  Player killerPlayer = killerview.gameObject.GetComponent<MultiplayerMoveAndShoot>().myPlayer;
        //  killerPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Kills", playerKills } });



    }
    [PunRPC] 
    void Disappear()
    {
        PlayerImage.SetActive(false);
    }
    [PunRPC]
    void Reappear()
    {
        PlayerImage.SetActive(true);
    }
   
    [PunRPC]
    private void DeathByBottom()
    {
      /*  PhotonView killerview = PhotonView.Find(currentshooterId);
        if (!killerview.IsMine)
        {
            killerview.gameObject.GetComponent<MultiplayerMoveAndShoot>().kills++;

            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Kills", kills } });
        }*/
         // last shooter is the killer when player is pushed or shot down


        if (view.IsMine)
        {
            soundHolder.EnemyDeath();
            PhotonNetwork.Instantiate(deatheffect.name, this.transform.position, Quaternion.identity);
            // myRend.enabled = false;  // image holder sprite rendeerfre
            deaths++;
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Deaths", deaths } });

            PlayerImage.SetActive(false);
            RespawnPanel.SetActive(true);
            guncontainer.SetActive(false);
            trail.enabled = false;
            shouldRespawn = true;
            
            //  

        }

    }
    public  GameObject PlayerImage;
    [PunRPC]
    public void Respawn()
    {
        
        health = 100;
        healthForLaser = 100;
        boostAmount = maxBoostValue;
      
        int rand = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[rand].transform.position; // spawnpoint are gotten from game tags and fixedupdate mehtod
        view.RPC(nameof(Reappear), RpcTarget.AllBuffered); // so that the players in the room can see you disapper and reappear
        // reenablecollider so that canbeKilled is set true once more to 
        // avoid registering more deaths is not working
        guncontainer.SetActive(true); // might be rpc'ed. 
        RespawnPanel.SetActive(false);
        trail.enabled = true;
        playerCanvas.SetActive(true);
        canDie = true;// also set camera to active true
    }

    IEnumerator waitbeforeRespawn()
    {
        trail.enabled = false;
        yield return new WaitForSeconds(5f);
        trail.enabled = true;
    }



    IEnumerator shineWhite()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        myRend.material = WhiteMaterial; // replace with image holders sprite rendeerer
        yield return new WaitForSeconds(0.05f);
        //   gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        gameObject.GetComponent<SpriteRenderer>().material = spritesDefault;

    }

    [PunRPC]
    public void IncreaseHealth()
    {
        if (health <= 80)
        {
            health += 20;
        }
        if (health > 80 && health <= 90)
        {
            health += 10;
        }
        if (health > 90)
        {
            health = 100;
        }

    }
    [PunRPC]
    public void IncreaseBoost()
    {
        boostAmount = maxBoostValue;
    }
   

     [PunRPC]
     public void TeleportPlayer()
     {
        trail.enabled = false;
        StartCoroutine(startTrail());
        int rand = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[rand].transform.position;
        
     }



    // Start is called before the first frame update
   

   
    IEnumerator startTrail()
    {
        trail.enabled = false;
        yield return new WaitForSeconds(4f);
        trail.enabled = true;
    }
    private void Update()
    {
      
        // if (view.IsMine) { }
        healthBar.value = health;
        
      /* if (!view.IsMine)
        {
            return;
        }
        if (Application.isFocused)
        {
            
        }
        if (!Application.isFocused)
        {
            SetPlayerInacative();
        }*/
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
        kills = GetComponent<KillCounter>().kills;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Kills", kills } });
        killerview = PhotonView.Find(currentshooterId);
       
        //  healthForLaser = (float)health;
        if (canDie)
        {
            if (health <= 0)
            {
                canDie = false;
                view.RPC(nameof(Dead), RpcTarget.AllBuffered); // Death issue solved 1. take damage method is
                // set to only reduce health if health is greater than zero in the first place.2. set a bool can die to false when 
                // health is less than or equal to zero in the fixed update method

            }
        }
       
       
        
        if (view.IsMine)
        {
           
            if (shouldRespawn == false)
            {
                CheckInput();
            }
           
          //  this.view.RPC(nameof(CheckInput), RpcTarget.AllBuffered); // recently added next up is testing network lattency using Ping and next multiplayer video on that
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if (shouldRespawn)
        {
            timePassed -= Time.deltaTime;
        }
        if (timePassed <= 0)
        {
            view.RPC(nameof(Respawn), RpcTarget.AllBuffered);
            timePassed = startTimer;
            shouldRespawn = false;
        }
        int intvallueTime = (int)timePassed;
        respawnText.text = "Respawning in....."+ intvallueTime.ToString()+"s";
        if (!view.IsMine)
            return;
        CheckInput();
      //  killedByText.text = killerview.player.nickname +"Killed You"; // will implement before launch

       /* if (photonView.IsMine) return;

         rb.position = Vector3.Lerp(rb.position, netposition, smoothPos * Time.fixedDeltaTime);
        transform.position= Vector3.Lerp(transform.position, netposition, smoothPos * Time.fixedDeltaTime);
        //  rb.rotation = Quaternion.Lerp(rb.rotation, netrotation, smoothRot * Time.fixedDeltaTime); ignore for now

        if (Vector3.Distance(rb.position,netposition)> teleportIfFarDistance)
        {
            rb.position = netposition;
            transform.position = netposition;
        }*/
        
    }

    [PunRPC]
    private void CheckInput()
    {
      /*  Physics2D.Simulate(Time in float);
        Physics2D.autoSimulation = true; an attempt at client side prediction */


        rb.MovePosition((Vector2)transform.position + direction * Speed * Time.deltaTime);
        
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
        if (collision.gameObject.tag == "BottomDeath")
        {
            health = 0;
            Instantiate(dustEffect, transform.position, Quaternion.identity);
            source.PlayOneShot(hitRock);
        }
       
        if (collision.gameObject.tag == "Health")
        {
            collision.gameObject.SetActive(false);
            soundHolder.collection();
            view.RPC(nameof(IncreaseHealth), RpcTarget.AllBuffered);
            PhotonNetwork.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Boost")
        {
            collision.gameObject.SetActive(false);
            soundHolder.collection();
            view.RPC(nameof(IncreaseBoost), RpcTarget.AllBuffered);
            PhotonNetwork.Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Portal")
        {
            collision.gameObject.SetActive(false);
            trail.enabled = false;
            soundHolder.collection();
            view.RPC(nameof(TeleportPlayer), RpcTarget.AllBuffered);
            PhotonNetwork.Destroy(collision.gameObject);

        }
    }
   
  /*   private Vector2 netposition;
    private Quaternion netrotation;
    private Vector3 previouspos;
    public bool teleportIfFar;
    public float teleportIfFarDistance;

    public float smoothPos = 5.0f;
    public float smoothRot = 5.0f;*/


      public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
      {
         /*
          if (stream.IsWriting)
          {

            
            
            // for transform vector
              stream.SendNext(transform.position);
            
            // for rigidbody 
              stream.SendNext(rb.position); // gives movement issues*****
              stream.SendNext(rb.rotation);
              stream.SendNext(rb.velocity);
            // for speed
            //  stream.SendNext(Speed);

          }
          else
          {
            
            // for transform vector
               transform.position= (Vector3)stream.ReceiveNext();
              
            // for rigidbody vector
              netposition = (Vector2)stream.ReceiveNext(); // rb.position is rigidbody2d
             netrotation = (Quaternion)stream.ReceiveNext();
              rb.velocity =   (Vector3)stream.ReceiveNext();

            // for speed 
            //  Speed = (float)stream.ReceiveNext();

            // for lag compensation
              float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
              netposition += (rb.velocity * lag);
          }
          */

      }
    





}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PowerUpSpawnMulti : MonoBehaviour
{
    public Transform[] spawnpoints;
    public GameObject[] powerupBoxes;
    public int powerupsSummoned=0;
    public bool canSpawn = true;

    // public float minTimeBtwSpawns;
    // public float maxTimeBtwSpawns;
    public float timebtwSpawns;


    public float maxBoxInRoom;

    // Start is called before the first frame update
    void Start()
    {
      
        Invoke("spawnBox", 0.5f); // start Loop
      
    }
    void spawnBox()
    {
        int index = Random.Range(0, spawnpoints.Length);
        Transform currentPoint = spawnpoints[index];
       
        int boxIndex = Random.Range(0, powerupBoxes.Length);

        if (canSpawn)
        {
            if (powerupsSummoned <= maxBoxInRoom)
            {
               PhotonNetwork.Instantiate(powerupBoxes[boxIndex].name, currentPoint.transform.position, Quaternion.identity);
                powerupsSummoned++;
            }
        }
        if (powerupsSummoned < maxBoxInRoom)
        {
            canSpawn = true;
            Invoke("spawnBox", timebtwSpawns);
        }
        if(powerupsSummoned >= maxBoxInRoom)
        {
            canSpawn = false;
            powerupsSummoned = 0;
            Debug.Log("Stop");
            StartCoroutine(SpawnMore());
        }
       
    }
    // Update is called once per frame
    IEnumerator SpawnMore()
    {
       
        yield return new WaitForSeconds(100f);
        canSpawn = true;
        Invoke("spawnBox", 0.5f); // restartLoop


    }

    void Update()
    {
       // powerupsInRoom = GameObject.FindGameObjectsWithTag("PowerUp").Length;

      /*  StartCoroutine(SpawnMore());

        if (canSpawn == false)
        {
            powerupsInRoom = 0;
            StartCoroutine(SpawnMore());
        }*/
    }
}

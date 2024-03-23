using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class polycoinScript : MonoBehaviour
{
    buttonSoundHolder soundHolder;
    // Start is called before the first frame update
    void Start()
    {
        soundHolder = FindObjectOfType<buttonSoundHolder>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundHolder.CoinSound();
            ScoreManager.instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10f);
    }
}

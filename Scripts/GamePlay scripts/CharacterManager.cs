using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDataBase characterDB;
    public SpriteRenderer artWorkSprite;
    
    private int selectedOption ;

    // Start is called before the first frame update
    void Start()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artWorkSprite.sprite = character.characterSprite;
        
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}

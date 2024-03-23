using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedName : MonoBehaviour
{
    public Text nameDisplay;
    public GameObject nameWindow;
    // Start is called before the first frame update
    void Start()
    {
        nameDisplay.text = PlayerPrefs.GetString("PlayerNickname");
    }

    // Update is called once per frame
    void Update()
    {
        nameDisplay.text = PlayerPrefs.GetString("PlayerNickname");
    }
    public void DisplayNameChange()
    {
        nameWindow.SetActive(true);
    }
    public void RemoveNameChange()
    {
        nameWindow.SetActive(false);
    }
}

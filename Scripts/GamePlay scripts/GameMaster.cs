using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameMaster : MonoBehaviour
{
   
    public GameObject restartPanel;
 //   public AudioClip buttonclip;
 //   public GameObject infoPanel;

    public GameObject settingsPanel,JoystickPostionPanel,coinShopPanel,Namewindow;
    public GameObject shopPanel;
    public GameObject statsPanel;
    public GameObject infoPanel;
    //   public GameObject loadingPanel;
    public GameObject playPanel;
    public GameObject LoadingText,LoadingObject;
    public GameObject map1Panel, map2Panel, map3Panel,map4Panel,map5Panel;
    public int hasDoneTutorial;

    private void Start()
    {
        hasDoneTutorial = PlayerPrefs.GetInt("hasDoneTutorial");
    }
    public void GotoMaps()
    {
        if (hasDoneTutorial == 1)
        {
            if (LoadingObject != null)
            {
                LoadingObject.SetActive(true);
            }

            SceneManager.LoadSceneAsync("Map Menu");
        }
        if (hasDoneTutorial == 0)
        {
            if (LoadingObject != null)
            {
                LoadingObject.SetActive(true);
            }
            SceneManager.LoadSceneAsync("Tutorial");
            PlayerPrefs.SetInt("hasDoneTutorial", 1);
            Namewindow.SetActive(true); // testing for now
        }

       
    }
    public void GotoMainMenu()
    {
        if (LoadingObject != null)
        {
            LoadingObject.SetActive(true);
        }
        SceneManager.LoadScene("Main menu");
    }
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    /*  public void Transition()
      {
          StartCoroutine(transition());
      }
      IEnumerator transition()
      {
          loadingPanel.SetActive(true);
          yield return new WaitForSeconds(2f);
          loadingPanel.SetActive(false);
      }*/

    public void Quit()
    {
        Debug.Log("Quit successful");
        Application.Quit();
    }
    /*  public void ShowInfo()
      {
          infoPanel.SetActive(true);
      }
      public void ShowGuide()
      {
          GuidePanel.SetActive(true); 
      }
      public void removeInfo()
      {
          infoPanel.SetActive(false);
      }*/

    /*  public void Restart()
      {
          Transition();
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
      public void GuideToMenu()
      {
          SceneManager.LoadSceneAsync("Main Menu");
      }
      public void GotoMoreLevelsMenu()
      {
          Transition();
          SceneManager.LoadSceneAsync("More Levels Menu");
      }
      public void GotoMainMenu()
      {
          Transition();
          SceneManager.LoadScene("Main Menu");
      }
      public void GotoLevelMenu()
      {
          Transition();
          SceneManager.LoadScene("Level Menu");
      } */
    public void toggleQuality(bool tog)
    {
        if (tog == true)
        {
            QualitySettings.SetQualityLevel(3,true);
        }
        else
        {
            QualitySettings.SetQualityLevel(0, true);
        }
    }
    public void ShopPanel()
    {
        if (shopPanel.activeSelf== true)
        {
            shopPanel.SetActive(false);
        }else
        if (shopPanel.activeSelf == false)
        {
            shopPanel.SetActive(true);
        }
    }
    public void Skins()
    {
        SceneManager.LoadSceneAsync("Skins");
    }
    public void GunsShop()
    {
        SceneManager.LoadSceneAsync("Guns Shop");
    }
    public void Tuturiol()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
    public void GoTomultiplayer ()
    {
        SceneManager.LoadSceneAsync("connecting  scene");
    }
    public void ActivatePlayPanel()
    {
        playPanel.SetActive(true);
    }
    public void deActivatePlayPanel()
    {
        playPanel.SetActive(false);
    }
    public void GotoLevelNumber ( string levelName)
    {
        LoadingText.SetActive(true);
        LoadingObject.SetActive(true);
        SceneManager.LoadScene(levelName);

    }
    public void DisplayMap1()
    {
        
      //  map1Panel.SetActive(true);
        map1Panel.GetComponent<WelldoneScaling>().ScaleUp();
        map2Panel.GetComponent<WelldoneScaling>().Scaledown();
       // map2Panel.SetActive(false);
        map3Panel.GetComponent<WelldoneScaling>().Scaledown();
        map4Panel.GetComponent<WelldoneScaling>().Scaledown();
        map5Panel.GetComponent<WelldoneScaling>().Scaledown();
        //  map3Panel.SetActive(false);
    }
    public void DisplayMap2()
    {
      //  map2Panel.SetActive(true);
        map2Panel.GetComponent<WelldoneScaling>().ScaleUp();
        map1Panel.GetComponent<WelldoneScaling>().Scaledown();
       // map1Panel.SetActive(false);
        map3Panel.GetComponent<WelldoneScaling>().Scaledown();
        map4Panel.GetComponent<WelldoneScaling>().Scaledown();
        map5Panel.GetComponent<WelldoneScaling>().Scaledown();
        //  map3Panel.SetActive(false);
    }

    public void DisplayMap3()
    {
        // map3Panel.SetActive(true);
        map3Panel.GetComponent<WelldoneScaling>().ScaleUp();
        map4Panel.GetComponent<WelldoneScaling>().Scaledown();
      
        map2Panel.GetComponent<WelldoneScaling>().Scaledown();
     //   map2Panel.SetActive(false);
        map1Panel.GetComponent<WelldoneScaling>().Scaledown();
        map5Panel.GetComponent<WelldoneScaling>().Scaledown();

        //  map1Panel.SetActive(false);
    }
    public void DisplayMap4()
    {
        // map3Panel.SetActive(true);
        map4Panel.GetComponent<WelldoneScaling>().ScaleUp();
        map3Panel.GetComponent<WelldoneScaling>().Scaledown();
        //   map2Panel.SetActive(false);
        map2Panel.GetComponent<WelldoneScaling>().Scaledown();
        map1Panel.GetComponent<WelldoneScaling>().Scaledown();
        map5Panel.GetComponent<WelldoneScaling>().Scaledown();
        //  map1Panel.SetActive(false);
    }
    public void DisplayMap5()
    {
        // map3Panel.SetActive(true);
        map5Panel.GetComponent<WelldoneScaling>().ScaleUp();
        map4Panel.GetComponent<WelldoneScaling>().Scaledown();
        map3Panel.GetComponent<WelldoneScaling>().Scaledown();
        //   map2Panel.SetActive(false);
        map2Panel.GetComponent<WelldoneScaling>().Scaledown();
        map1Panel.GetComponent<WelldoneScaling>().Scaledown();
        //  map1Panel.SetActive(false);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    public void removeSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void CoinsPanelActivate()
    {
        coinShopPanel.SetActive(true);
    }
    public void CoinsPanelDeactivate()
    {
        coinShopPanel.SetActive(false);
    }
    public void JoystickPos()
    {
        JoystickPostionPanel.SetActive(true);
    }
    public void removeJoystickPos()
    {
        JoystickPostionPanel.SetActive(false);
    }
    public void Stats()
    {
        statsPanel.SetActive(true);
    }
    public void Info()
    {
        infoPanel.SetActive(true);
    }
    public void RemoveStats()
    {
        statsPanel.SetActive(false);
    }
    public void RemoveInfo()
    {
        infoPanel.SetActive(false);
    }
}

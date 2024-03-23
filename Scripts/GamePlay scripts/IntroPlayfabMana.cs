using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class IntroPlayfabMana : MonoBehaviour
{
    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject leaderboardWindow;
    public GameObject LoggingInPanel;

    [Header("Display name window")]
    public GameObject nameError;
    public InputField nameInput;

    [Header("Leaderboard")]
    public GameObject rowPrefab;
    public Transform rowsParent;
    bool ison = true;

    string loggedInplayfabID;
    public GameObject noInternetpanel;
    public Button PlayButton,LeaderboardButton;
    // Start is called before the first frame update

     // To Check for internet connection before loging in
    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null) // i.e if there is an error...
        {
            noInternetpanel.SetActive(true);
            PlayButton.interactable = false;
            LeaderboardButton.interactable = false;
        }
        else
        {
          //  PlayButton.interactable = true; // lets check from login first
            noInternetpanel.SetActive(false);
            Login();
            GetLeaderboardAroundPlayer();
        }
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Start()
    {
        /*  if (Application.internetReachability == NetworkReachability.NotReachable)
          {
              noInternetpanel.SetActive(true);
              Debug.Log("Error. Check internet connection!");
          }
          else
          {
              Login();
              noInternetpanel.SetActive(false);
          }
         */
      
        StartCoroutine(CheckInternetConnection());
        if (this.isActiveAndEnabled)
        {
            GetLeaderboardAroundPlayer();
        }
       
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
    void Login()
    {
        LoggingInPanel.SetActive(true);
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result)
    {
        loggedInplayfabID = result.PlayFabId;
        PlayButton.interactable = true;
        LeaderboardButton.interactable = true;
        LoggingInPanel.SetActive(false);
        Debug.Log("Successful login/account created!");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        PlayerPrefs.SetString("PlayerNickname", name);
        if (name == null)
            nameWindow.SetActive(true);
         else
          nameWindow.SetActive(false);
    }
    public void SubmitNameButton()
    {
        if(nameInput.text.Length >= 1 && nameInput.text.Length < 15)
        {
            PlayerPrefs.SetString("PlayerNickname", nameInput.text);
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = PlayerPrefs.GetString("PlayerNickname"),

            };

            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
            nameWindow.SetActive(false);
        }
      
    }
    public void DeleteAccount()
    {
        
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
      //  leaderboardWindow.SetActive(true);
    }
    void OnError(PlayFabError error)
    {
        noInternetpanel.SetActive(true);
        Debug.Log("Error while logging in / creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
    public void ActivateLeaderboard()
    {
        if (ison)
        {
            leaderboardWindow.SetActive(true);
            GetLeaderboardAroundPlayer();

            ison = false;
        }
        else
        if (ison == false)
        {
            leaderboardWindow.SetActive(false);
            GetLeaderboardAroundPlayer();
            ison = true;
        }
    }
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "PlatformScore",
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }
    // for getting the top players
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInplayfabID)
            {
                texts[0].color = Color.cyan;
                texts[1].color = Color.cyan;
                texts[2].color = Color.cyan;
            }

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
    // for getting scores around player

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInplayfabID)
            {
                texts[0].color = Color.cyan;
                texts[1].color = Color.cyan;
                texts[2].color = Color.cyan;
            }
            /*  if (loggedInplayfabID==item.PlayFabId)
              {
                  texts[0].color = Color.red;
                  texts[1].color = Color.cyan;
                  texts[2].color = Color.cyan;
              }*/
              
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
    // Logging in registering using Username and PassWord
    [Header("UI")]
    public Text MessageText;
    public InputField emailInput;
    public InputField passWordInput;

    public void RegisterButton()
    {
        if (passWordInput.text.Length < 6)
        {
            MessageText.text = "Password too short!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passWordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser (request,OnRegisterSuccess,OnErrorRegister);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        MessageText.text = "Registered and Logged in!";
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passWordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnErrorRegister);
    }
    void OnLoginSuccess( LoginResult result)
    {
        MessageText.text = "Logged In!";
        Debug.Log("Successful login / account created!");
    }
    public void ResetPassWordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "" // input this later

        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnErrorRegister);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        MessageText.text = "Password reset mail sent!";
    }
    void OnErrorRegister( PlayFabError error)
    {
        MessageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System;

public class PlayerRegister : MonoBehaviour
{
    public Button joinButton;
    public CanvasGroup _cGroup;

    [SerializeField] private TMP_InputField usernameInput=null;
    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private Image welcomeImage;

    private const string playerNamePref = "PlayerName";
    

    // Start is called before the first frame update
    void Awake()
    {
        SetupLoginScreen();
    }

    private void SetupLoginScreen()
    {
        if(!PlayerPrefs.HasKey(playerNamePref))
        {
            Debug.LogError("No player with username " + playerNamePref + " found");
            return;
        }

        string defaultPlayerName = PlayerPrefs.GetString(playerNamePref);

        usernameInput.text = defaultPlayerName;

        SetPlayerName(defaultPlayerName);
    }

    public void SetPlayerName(string name)
    {
        if (name.Length < 5)
        {
            Debug.LogError("Player name must be greater than 5 characters");
            joinButton.interactable = false;
            return;
        }

        else
        {
            joinButton.interactable = true;
        }

        
    }

    public void SavePlayerDetails()
    {
        string _playerName = usernameInput.text;

        PhotonNetwork.NickName = _playerName;

        PlayerPrefs.SetString(playerNamePref, _playerName);

        Debug.Log("Player Name " + _playerName + " saved successfully");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

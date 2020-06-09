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
    public GameObject registerCanvas;
    public GameObject mainMenu;

    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private TMP_Text welcomeText;


    private readonly string playerNamePref = "PlayerName";

    private Animator welcomeAnim;

    // Start is called before the first frame update
    void Awake()
    {
        welcomeAnim = welcomeScreen.GetComponent<Animator>();
        welcomeText = welcomeScreen.GetComponentInChildren<TMP_Text>();
        SetupLoginScreen();
        registerCanvas.SetActive(true);
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

        StartCoroutine(StartFadeAnim(_playerName));

        
    }

    IEnumerator StartFadeAnim(string name)
    {
        

        welcomeAnim.SetTrigger("FadeOut");

        welcomeText.text = "Welcome " + name+" !";

        

        yield return new WaitForSeconds(2f);

        welcomeAnim.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.2f);

        mainMenu.SetActive(true);

        registerCanvas.SetActive(false);

        


    }

    
}

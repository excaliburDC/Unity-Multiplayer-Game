using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Matchmake : MonoBehaviourPunCallbacks
{
    public GameObject mainMenuPanel;
    public GameObject findOpponentpanel;

    [SerializeField] private TextMeshProUGUI waitingStatusText;

    private bool isConnecting = false;
    

    //if game versions of players don't match, players can't join the game 
    private readonly string gameVersion = "0.1";

    private const int maxPlayersPerRoom = 2;
    private const float countdown = 5f;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        waitingStatusText.text = "";
    }

    public void FindOpponent()
    {
        
        isConnecting = true;

        mainMenuPanel.SetActive(false);
        findOpponentpanel.SetActive(true);

        waitingStatusText.text = "Waiting for players to join...\n\nPlease wait....";

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }

        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

        StartCoroutine(DisconnectFailure());

        Debug.LogError($"Disconnected due to : {cause}");



    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("No room found...Creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        StopAllCoroutines();
        Debug.Log("Client Successfully joined a room...");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != maxPlayersPerRoom)
        {
            waitingStatusText.text = "Finding an opponent...\n\nPlease wait....";
            Debug.Log("Client waiting for an opponent");
        }

        else
        {
            waitingStatusText.text = "Opponent found !";
            Debug.Log("Match about to begin");
            StartCoroutine(LoadGame());
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount==maxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            waitingStatusText.text = "Opponent found !";
            Debug.Log("Match about to begin");

            StartCoroutine(LoadGame());
        }
    }

    private IEnumerator DisconnectFailure()
    {
        waitingStatusText.text = "Failed to Find an opponent\n\n  Going back to Main Menu";

        yield return new WaitForSeconds(2f);

        findOpponentpanel.SetActive(false);

        mainMenuPanel.SetActive(true);
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);

        float curCount = countdown;

        while(curCount>0f)
        {
            Debug.Log("Match about to begin in:" + curCount);
            waitingStatusText.text = "Match about to begin in\n\n" + curCount;
            yield return new WaitForSeconds(1f);
            curCount--;
        }

        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

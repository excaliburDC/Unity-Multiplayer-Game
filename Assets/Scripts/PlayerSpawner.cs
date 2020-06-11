using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject player1Prefab;


    void Start()
    {
        PhotonNetwork.Instantiate(player1Prefab.name, transform.position, Quaternion.identity);
    }

    
}

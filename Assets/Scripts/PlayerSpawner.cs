using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private Vector3 offset = new Vector3(2f, 0f, 0f);


    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(player1Prefab.name, transform.position+offset, Quaternion.identity);
        }

        else
        {
            PhotonNetwork.Instantiate(player2Prefab.name, transform.position-offset, Quaternion.identity);
        }
        
    }

    
}

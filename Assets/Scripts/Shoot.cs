using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shoot : MonoBehaviourPun
{
    [SerializeField] private float laserForce = 5f;
    [SerializeField] private Transform spawnPoint;

    private Rigidbody2D rb2d;



    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if(photonView.IsMine)
        {
            FireInput();
        }
          
    }

    void FireInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("ShootLasers", RpcTarget.All);
        }
    }

    [PunRPC]
    void ShootLasers()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject player1Laser = PoolManager.Instance.SpawnInWorld("laser1", spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D laser1Rb = player1Laser.GetComponent<Rigidbody2D>();
            laser1Rb.velocity = spawnPoint.up * laserForce;
        }

        else
        {
            GameObject player2Laser = PoolManager.Instance.SpawnInWorld("laser2", spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D laser2Rb = player2Laser.GetComponent<Rigidbody2D>();
            laser2Rb.velocity = spawnPoint.up * laserForce;
        }



    }


    
}

using System;
using UnityEngine;
using Photon.Pun;


public class Shoot : MonoBehaviourPun
{

    [SerializeField] private float laserForce = 6f;
    [SerializeField] private Transform spawnPoint;

    private Rigidbody2D rb2d;




    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            FireInput();
        }

        //TestFire();

        

    }

    void FireInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("ShootLasers", RpcTarget.All);
        }
    }

    //for testing only
    //void TestFire()
    //{
    //    if(Input.GetButtonDown("Fire1"))
    //    {
    //        ShootLasers();
    //    }
    //}

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

        ////for testing
        //GameObject player1Laser = PoolManager.Instance.SpawnInWorld("laser1", spawnPoint.position, spawnPoint.rotation);
        //Rigidbody2D laser1Rb = player1Laser.GetComponent<Rigidbody2D>();
        //laser1Rb.velocity = spawnPoint.up * laserForce;


    }


    
}

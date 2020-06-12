using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shoot : MonoBehaviourPun
{
    public float laserForce = 5f;

    [SerializeField] private Transform spawnPoint;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        SpawnLasers();
    }

    //[PunRPC]
    void SpawnLasers()
    {
       GameObject player1Laser = PoolManager.Instance.SpawnInWorld("laser1", spawnPoint.position, spawnPoint.rotation);
        // PoolManager.Instance.SpawnInWorld("laser2", spawnPoint.position, spawnPoint.rotation);

        
    }



    
}

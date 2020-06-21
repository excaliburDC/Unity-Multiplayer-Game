using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollisionChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Asteroid")
        {
            Asteroid asteroid = col.gameObject.GetComponentInParent<Asteroid>();
            asteroid.TakeDamage();
            gameObject.SetActive(false);
            
        }
    }
}

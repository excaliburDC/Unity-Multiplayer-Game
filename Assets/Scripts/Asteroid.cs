using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomRandomGenExtension;
using Photon.Pun;

[RequireComponent(typeof(WrappingEffect))]
public class Asteroid : MonoBehaviourPun
{
	public event System.Action<Asteroid,Vector3, List<GameObject>> EventDestroyed;

	public int points;

	public  List<GameObject> childAsteroid;

	public List<GameObject> asteroids;

	public float asteroidMoveSpeed = 1f;




	private void Start()
	{
		if (photonView.IsMine)
		{
			photonView.RPC("SelectActiveAsteroid", RpcTarget.All);
		}
		//SelectActiveAsteroid();
	}

	private void Update()
	{
		if (photonView.IsMine)
			photonView.RPC("AsteroidMovement", RpcTarget.All);

		//AsteroidMovement();

		//if(PhotonNetwork.IsMasterClient)
		//{
		//	AsteroidMovement();
		//}

	}

	public void SelectActiveAsteroid()
	{
		foreach (GameObject asteroidObj in asteroids)
		{
			GameObject currentObj = asteroidObj;
			currentObj.SetActive(false);
		}

		//GameObject selectedAsteroid = asteroids[Random.Range(0, asteroids.Count)];
		GameObject selectedAsteroid = asteroids[RandomIndexGenerator.GetRandomIndex(asteroids)];
		Debug.Log(selectedAsteroid.name);
		selectedAsteroid.SetActive(true);

	}

	//[PunRPC]
	public void AsteroidMovement()
	{
		transform.Translate(transform.up * asteroidMoveSpeed * Time.deltaTime, Space.World);
	}

	public void TakeDamage()
	{
		if (EventDestroyed != null)
		{
			if (childAsteroid == null)
			{
				Debug.Log("The asteroid can't be broken any further");
				return;
			}
			EventDestroyed(this, transform.position, childAsteroid);
			Debug.Log("Asteroid"+this.gameObject.name+ " destroyed");
			gameObject.SetActive(false);
		}

	}




}

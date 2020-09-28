using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class AsteroidSpawner : MonoBehaviour
{
	
	[SerializeField] private GameObject asteroidPrefab;
	[SerializeField] private float offscreenOffset;
	[SerializeField] private int startAsteroidCount = 2;


	
	private Camera cam;
	public List<Asteroid> asteroidsList; //keeps track of no of asteroid in the game

	public int AsteroidsRemaining
	{
		get { return asteroidsList.Count; }
	}

	private void Awake()
	{
		cam = Camera.main;
	
	}

	// Start is called before the first frame update
	void Start()
    {
		SpawnAsteroids();
		//photonView.RPC("SpawnAsteroids", RpcTarget.All);
	}


	//[PunRPC]
	public void SpawnAsteroids()
	{
		int numAsteroids = startAsteroidCount;

		for (int i = 0; i < numAsteroids; i++)
		{
			Vector3 offScreenPos = GetOffScreenPosition();
			Quaternion offScreenRot = GetOffScreenRotation();

			//if(photonView.IsMine)
   //         {
			//	photonView.RPC("InitAsteroids", RpcTarget.All,asteroidPrefab,offScreenPos,offScreenRot);

			//}
			InitAsteroids(asteroidPrefab, offScreenPos, offScreenRot);

			
		}

	}

	public void ResetAsteroid()
	{
		if (asteroidsList != null)
		{
			foreach (Asteroid ast in asteroidsList)
			{
				ast.gameObject.SetActive(false);
			}
		}
		asteroidsList = new List<Asteroid>();
	}


	private Vector3 GetOffScreenPosition()
	{
		float posX = 0.0f;
		float posY = 0.0f;
		int startingSide = Random.Range(0, 4);
		switch (startingSide)
		{
			// top
			case 0:
				posX = Random.value;
				posY = 0.0f;
				posY -= offscreenOffset;
				break;
			// bottom
			case 1:
				posX = Random.value;
				posY = 1.0f;
				posY += offscreenOffset;
				break;
			// left
			case 2:
				posX = 0.0f;
				posY = Random.value;
				posX -= offscreenOffset;
				break;
			// right
			case 3:
				posX = 1.0f;
				posY = Random.value;
				posX += offscreenOffset;
				break;
		}
		return cam.ViewportToWorldPoint(new Vector3(posX, posY, 1.0f));
	}

	private Quaternion GetOffScreenRotation()
	{
		int angle = 0;
		int startingSide = Random.Range(0, 4);
		switch (startingSide)
		{
			case 0:
				angle = Random.Range(20, 70);
				break;
			case 1:
				angle = -Random.Range(20, 70);
				break;
			case 2:
				angle = Random.Range(110, 160);
				break;
			case 3:
				angle = -Random.Range(110, 160);
				break;
		}
		return Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
	}


	//[PunRPC]
	private void InitAsteroids(GameObject prefab,Vector3 position,Quaternion rotation)
	{

		GameObject asteroidObj = PhotonNetwork.InstantiateSceneObject(prefab.name, position, rotation);

		Debug.Log(asteroidObj);

		asteroidObj.transform.SetParent(gameObject.transform);

		Asteroid astObj = asteroidObj.GetComponent<Asteroid>();


		//astObj.SelectActiveAsteroid();

		astObj.EventDestroyed += OnAsteroidDie;

		asteroidsList.Add(astObj);

		
	}

	private void OnAsteroidDie(Asteroid asteroid,Vector3 position, List<GameObject> childAsteroids)
	{
		asteroidsList.Remove(asteroid);

		for (int i = 0; i < childAsteroids.Count; i++)
		{
			// create children asteroids
			Quaternion rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Floor(Random.Range(0.0f, 360.0f))));
			InitAsteroids(childAsteroids[i], position, rotation);
			//photonView.RPC("InitAsteroids", RpcTarget.All, childAsteroids[i],position,rotation);
		
			
		}
		
		

		//// dispatch event
		//if (EventAsteroidDestroyed != null)
		//{
		//	EventAsteroidDestroyed(points);
		//}
	}

	//public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	//{
	//	if (stream.IsWriting)
	//	{
	//		stream.SendNext(asteroidPrefab);

	//	}

	//	else
	//	{
	//		asteroidPrefab = (GameObject)stream.ReceiveNext();
	//	}
	//}
}
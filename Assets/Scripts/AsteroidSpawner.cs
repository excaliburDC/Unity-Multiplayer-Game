using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	//[SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float offscreenOffset;

	private Camera cam;
	public List<Asteroid> asteroidsList; //keeps track of no of asteroid in the game

	private void Awake()
	{
		cam = Camera.main;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
		{
			SpawnAsteroids("AsteroidBig",GetOffScreenPosition(),GetOffScreenRotation());
		}

	
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

	private void SpawnAsteroids(string prefabName,Vector3 position,Quaternion rotation)
	{
		GameObject asteroid = PoolManager.Instance.SpawnInWorld(prefabName, position, rotation);

		Asteroid astObj = asteroid.GetComponent<Asteroid>();


		//astObj.SelectActiveAsteroid();

		astObj.eventDestroyed += OnAsteroidDie;

		asteroidsList.Add(astObj);

		
	}

	private void OnAsteroidDie(Asteroid asteroid,Vector3 position, GameObject childAsteroids)
	{
		asteroidsList.Remove(asteroid);

		// create children asteroids
		Quaternion rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Floor(Random.Range(0.0f, 360.0f))));
		SpawnAsteroids(childAsteroids.name, position, rotation);
		

		//// dispatch event
		//if (EventAsteroidDestroyed != null)
		//{
		//	EventAsteroidDestroyed(points);
		//}
	}




}

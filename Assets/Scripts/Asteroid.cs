using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomRandomGenExtension;

[RequireComponent(typeof(ShipWrappingEffect))]
public class Asteroid : MonoBehaviour
{
	public event System.Action<Asteroid,Vector3, GameObject> eventDestroyed;

	public int points;

	public GameObject childAsteroid;

	public List<GameObject> asteroids;


	private void Start()
	{
		SelectActiveAsteroid();
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (eventDestroyed != null)
			{
				if (childAsteroid == null)
				{
					Debug.Log("The asteroid can't be broken any further");
					return;
				}
				eventDestroyed(this,transform.position, childAsteroid);
				Debug.Log("Asteroid {0} destroyed" + this.gameObject.name);
			}
		}
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




}

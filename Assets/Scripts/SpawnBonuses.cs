using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn

	private float nextSpawnTime;

	void Start ()
	{
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	void Update ()
	{
		// add exit if the game is over
		// if()

		if (Time.time  >= nextSpawnTime) {
			MakeThingToSpawn ();
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}	
	}

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

		spawnPosition.x = Random.Range (xMinRange, xMaxRange);
		spawnPosition.y = Random.Range (yMinRange, yMaxRange);
        spawnPosition.z = 0;

		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

		spawnedObject.transform.parent = gameObject.transform;
	}
}

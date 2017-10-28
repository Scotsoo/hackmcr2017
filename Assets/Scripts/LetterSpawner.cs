using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour {

	public float spawnFrequency = 1.0f; // spawn frequency in seconds
	public float spawnRadius = 10.0f; // spawn radius around player
	public float spawnDuration = 5.0f; // life of spawned objects
	public GameObject spawnCenterObject; // object to spawn around
	public GameObject[] spawnObjects; // array of prefabs to generate

	private float lastSpawnTime;

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
	}
	
	void FixedUpdate () {
		float timeNow = Time.time;
		if (timeNow - lastSpawnTime > spawnFrequency) {
			SpawnRandomLetter();
			lastSpawnTime = timeNow;
		}
	}

	void SpawnRandomLetter () {
		// select a random prefab from spawnObjects
		int numObjects = spawnObjects.Length;

		if (numObjects > 0) {
			int index = Random.Range(0, numObjects);
			Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
			Vector3 spawnCenter = spawnCenterObject.transform.position;

			Vector3 randomPosition = new Vector3(
				spawnCenter.x + randomOffset.x,
				1.0f,
				spawnCenter.z + randomOffset.y
			);

			Vector3 spawnFacing = new Vector3(1.0f, 0.0f, 0.0f);
			GameObject spawnedObject = Instantiate(spawnObjects[index], randomPosition, Quaternion.LookRotation(spawnFacing));
			spawnedObject.transform.parent = this.transform;
			spawnedObject.AddComponent<DestroySelfAfterTime>();
			spawnedObject.GetComponent<DestroySelfAfterTime>().lifeInSeconds = spawnDuration;
		}
	}
}

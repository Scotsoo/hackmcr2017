using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterTime : MonoBehaviour {

	public float lifeInSeconds = 1.0f;

	private float creationTime;

	// Use this for initialization
	void Start () {
		creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - creationTime > lifeInSeconds) {
			Object.Destroy(this.gameObject);
		}
	}
}

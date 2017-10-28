using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPinKeyboardControl : MonoBehaviour {

	private Vector3 velocity = Vector3.zero;
	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.velocity = Vector3.zero;

		if (Input.GetKey(KeyCode.W)) {
			this.velocity += new Vector3(this.speed, 0, 0);
		}
		if (Input.GetKey(KeyCode.S)) {
			this.velocity -= new Vector3(this.speed, 0, 0);
		}
		if (Input.GetKey(KeyCode.A)) {
			this.velocity += new Vector3(0, 0, this.speed);
		}
		if (Input.GetKey(KeyCode.D)) {
			this.velocity -= new Vector3(0, 0, this.speed);
		}

		this.transform.position += this.velocity;
	}
}

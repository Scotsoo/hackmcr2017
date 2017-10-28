using Assets.Scripts;
using UnityEngine;

public class PlayerPinKeyboardControl : MoanymonBehaviour {

	private Vector3 _velocity = Vector3.zero;
	public float Speed = 0.1f;

	// Use this for initialization
	//void Start () {
	//	this.camera = GameObject.FindGameObjectWithTag("MainCamera");
	//}
	
	// Update is called once per frame
	void Update () {
		_velocity = Vector3.zero;

		if (Input.GetKey(KeyCode.W)) {
			_velocity += new Vector3(Speed, 0, 0);
		}
		if (Input.GetKey(KeyCode.S)) {
			_velocity -= new Vector3(Speed, 0, 0);
		}
		if (Input.GetKey(KeyCode.A)) {
			_velocity += new Vector3(0, 0, Speed);
		}
		if (Input.GetKey(KeyCode.D)) {
			_velocity -= new Vector3(0, 0, Speed);
		}

		transform.position += _velocity;
		Camera.transform.position += this._velocity;
	}
}

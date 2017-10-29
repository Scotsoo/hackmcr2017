using UnityEngine;

public class PlayerPinKeyboardControl : MoanymonBehaviour {

	private Vector3 _velocity = Vector3.zero;
	public float Speed = 112000.0f;
	private bool locationInitialised = false;
	 
	private Vector2 prevLatLon = Vector2.zero;

	void Start () {
		base.Start();
        Input.location.Start(1,1);
	}
	
	// Update is called once per frame
	void Update () {
		_velocity = Vector3.zero;

		if (locationInitialised) {
			Vector2 newLatLon = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
			Vector2 deltaLocation = newLatLon - prevLatLon;

			this._velocity = new Vector3(deltaLocation.x * Speed, 0.0f, deltaLocation.y * Speed);

			Debug.Log(prevLatLon.x + "," + prevLatLon.y + "|" + newLatLon.x + "," + newLatLon.y + "|" + _velocity.x + "," + _velocity.z);

			prevLatLon = newLatLon;
		}

        if (!locationInitialised) {
			if (Input.location.status == LocationServiceStatus.Failed) {
				print("Unable to determine device location");
			} else if (Input.location.status != LocationServiceStatus.Initializing) {
				locationInitialised = true;
				prevLatLon = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
			}
		}

		// if (Input.GetKey(KeyCode.W)) {
		// 	_velocity += new Vector3(Speed, 0, 0);
		// }
		// if (Input.GetKey(KeyCode.S)) {
		// 	_velocity -= new Vector3(Speed, 0, 0);
		// }
		// if (Input.GetKey(KeyCode.A)) {
		// 	_velocity += new Vector3(0, 0, Speed);
		// }
		// if (Input.GetKey(KeyCode.D)) {
		// 	_velocity -= new Vector3(0, 0, Speed);
		// }

		transform.position += _velocity;
		Camera.transform.position += this._velocity;
	}
}

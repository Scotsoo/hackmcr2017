using System;
using UnityEngine;

public class PlayerPinKeyboardControl : MoanymonBehaviour
{
	public float Speed = 112000.0f;
    private Vector2 _prevLatLon;

    new void Start () {
		base.Start();
	    Input.location.Start(10, 0.1f); // accuracy, every 0.1m
        _prevLatLon = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
	    LocationServiceStatus currStatus = Input.location.status;
        Vector3 velocity = Vector3.zero;

	    switch (currStatus)
	    {
	        case LocationServiceStatus.Initializing:
                Debug.Log("Starting GPS...");
	            break;
	        case LocationServiceStatus.Running:
                Debug.Log("Calculating new position...");
	            velocity = CalculateNewPosition();
	            break;
	        case LocationServiceStatus.Stopped:
	        case LocationServiceStatus.Failed:
	        default:
	            Debug.Log("No GPS available");
	            break;
	    }

	    if (velocity != Vector3.zero)
	    {
	        transform.position += velocity;
	        Camera.transform.position += velocity;
        }
	}

    Vector3 CalculateNewPosition()
    {
        Vector2 newLatLon = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        Debug.Log(string.Format("Current Location - Lat: {0} Long: {1}", newLatLon.x, newLatLon.y));
        Vector2 delta = newLatLon - _prevLatLon;
        Debug.Log(string.Format("Delta change - Lat: {0} Long: {1}", delta.x, delta.y));
        Vector3 velocity = new Vector3(delta.x * Speed, 0, delta.y * Speed);
        Debug.Log(string.Format("Velocity calculated - X: {0}, Y: {1}, Z: {2}", velocity.x, velocity.y, velocity.z));

        _prevLatLon = newLatLon;

        return velocity;
    }
}

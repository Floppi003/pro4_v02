using UnityEngine;
using System.Collections;

public class Maze_Collider : MonoBehaviour {

	//public Vector3 angle;
	public float angle;
	private float playerViewAngle;
	public Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frames
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
		//	Debug.Log ("Player detected");

			//Quaternion wantedAngle = Quaternion.Euler(angle);

			angle = transform.rotation.y;
			playerViewAngle = cam.transform.rotation.y;

			if ((playerViewAngle <= angle-5) && (playerViewAngle >= angle+5)) {
				Debug.LogError("Falscher Winkel.");
			} else {
				Debug.LogError("Richtiger Winkel");
			}
			Debug.Log("View: " + playerViewAngle);
			Debug.Log("Collider: " + angle);

		}
	//	Debug.Log ("OnTriggerStay");
	}
}

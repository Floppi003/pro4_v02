using UnityEngine;
using System.Collections;

public class Maze_Collider : MonoBehaviour {

	public Vector3 angle;
	public Vector3 playerViewAngle;
	public Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			Debug.Log ("Player detected");

			playerViewAngle = cam.transform.rotation;
			// quaternion to vector 3 oder so, zum vergleichen
		}
		Debug.Log ("OnTriggerStay");
	}
}

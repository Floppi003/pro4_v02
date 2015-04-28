using UnityEngine;
using System.Collections;

public class Maze_Collider : MonoBehaviour {

	public Vector3 angle;
	public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			Debug.Log ("Player detected");

		}
		Debug.Log ("OnTriggerStay");
	}
}

using UnityEngine;
using System.Collections;

public class BridgeCollider : MonoBehaviour {

	public string objectName;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);
	}
	
	void OnTriggerStay(Collider other) {
		Debug.Log ("onTriggerStay");

		// if it is the bridge, move the player slightly
		if (objectName.Equals ("Bridge")) {
			float randomNumber = Random.Range (-0.1f, 0.00f);
			Vector3 position = other.gameObject.transform.position;
			other.gameObject.transform.position = new Vector3 (position.x, position.y, position.z + randomNumber);

		// if it is the floor below the bridge, move the player back to the beginning
		} else if (objectName.Equals ("BridgeFloorless")) {
			other.gameObject.transform.position = new Vector3(25.1f, 1.32f, -0.61f);
		}


	}
	
	void OnTriggerExit(Collider other) {
		
	}
	
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

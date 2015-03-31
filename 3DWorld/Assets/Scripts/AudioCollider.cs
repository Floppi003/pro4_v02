using UnityEngine;
using System.Collections;

public class AudioCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter");
		other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);
	}

	void OnTriggerStay(Collider other) {

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

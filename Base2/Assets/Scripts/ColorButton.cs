using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collider other){
		if (other.tag == "ColorButton") {
			Debug.Log ("Button!!!!!!!!");

			other.GetComponentsInChildren<MeshRenderer>().


			other.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Button1", typeof(Material));
		}
		Debug.Log ("OnTriggerEnter");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);
		
	}
}

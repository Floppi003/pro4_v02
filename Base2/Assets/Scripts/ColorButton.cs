using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {

	public Material selectedMaterial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionStay(Collision other){
		/*Material mat_button = Instantiate(Resources.Load("Button", typeof(Material))) as Material;
		Material mat_button1 = Instantiate(Resources.Load("Button1", typeof(Material))) as Material;

		if (other.gameObject.tag == "RedButton") {
			Debug.LogError ("Red Button!!!!!!!!");
		
			other.gameObject.GetComponent<Renderer>().material = mat_button;

		} else if (other.gameObject.tag == "GreenButton") {
			Debug.LogError ("Green Button!!!!!!!!");
			
			other.gameObject.GetComponent<Renderer>().material = mat_button;

		} else if (other.gameObject.tag == "BlueButton") {
			Debug.LogError ("Blue Button!!!!!!!!");
			
			other.gameObject.GetComponent<Renderer>().material = mat_button;
		}*/
//		Debug.LogError ("OnCollidionStay");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);
		
//	}

	void OnTriggerStay(Collider other) {
		//Material mat_button = Instantiate(Resources.Load("Button", typeof(Material))) as Material;
		//Material mat_button1 = Instantiate(Resources.Load("Button1", typeof(Material))) as Material;
		
		if (other.gameObject.tag == "RedButton") {
			Debug.LogError ("Red Button!!!!!!!!");
//			other.gameObject.GetComponent<Renderer>().material = mat_button;
			
		} else if (other.gameObject.tag == "GreenButton") {
			Debug.LogError ("Green Button!!!!!!!!");
			
//			other.gameObject.GetComponent<Renderer>().material = mat_button;
			
		} else if (other.gameObject.tag == "BlueButton") {
			Debug.LogError ("Blue Button!!!!!!!!");
			
//			other.gameObject.GetComponent<Renderer>().material = mat_button;
		}
		other.material = selectedMaterial;

		Debug.Log ("OurOnTriggerStay");	
	}
}

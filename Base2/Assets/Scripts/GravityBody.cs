using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	public float gravity = -9.8f;
	public Vector3 gravityUp = new Vector3(0,1,0);

	GravityAttractor targetGravity;
	bool planetGravity = false;
	GameObject player;
	GameObject[] planets;
	GameObject targetPlanet;
	
	void Awake () {
		
		planets = GameObject.FindGameObjectsWithTag ("Planet");
		if (planets.Length > 0) {
			planetGravity = true;
			
			player = GameObject.FindGameObjectWithTag ("Player");
			targetPlanet = planets [0];
			//planets = GameObject.FindGameObjectsWithTag("Planet").GetComponent<GravityAttractor>();
			//planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
			
			// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
			
			InvokeRepeating ("FindTargetPlanet", 2.0f, 0.5f);
		} else {
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
	
	void FindTargetPlanet(){
		float targetDistance = Vector3.Distance (targetPlanet.transform.position, player.transform.position);	
		for(var i = 0; i < planets.Length; i++)
		{
			GameObject tmpPlanet = planets[i];
			float tmpDistance = Vector3.Distance (tmpPlanet.transform.position, player.transform.position);
			if(tmpDistance < targetDistance)
			{
				targetPlanet = tmpPlanet;
			}
		}
	}
	
	//FixedUpdate gets called at a regular interval independent from the framerate
	void FixedUpdate () {
		if (planetGravity) {
			targetGravity = targetPlanet.GetComponent<GravityAttractor> ();
			targetGravity.Attract (transform);
		} else {
			Vector3 localUp = transform.up; //object gravity
			
			// Apply downwards gravity to body
			transform.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
			// Allign bodies up axis with the centre of planet
			transform.rotation = Quaternion.FromToRotation(gravityUp,gravityUp) * transform.rotation;
		}
		//linear interpolation
		//Zielposition über Zeitraum
	}
}
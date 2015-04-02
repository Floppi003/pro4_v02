using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	GravityAttractor targetGravity;
	GameObject player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		//planets = GameObject.FindGameObjectsWithTag("Planet").GetComponent<GravityAttractor>();
		//planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		
		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}

	//gets called at a regular interval independent on the framerate
	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity

		var planets = GameObject.FindGameObjectsWithTag("Planet");
		GameObject targetPlanet = planets[0];
		float targetDistance = Vector3.Distance (targetPlanet.transform.position, player.transform.position);
		//Debug.Log ("collided gameobject name: " + hit.collider.gameObject.name);

		for(var i = 1; i < planets.Length; i++)
		{
			GameObject tmpPlanet = planets[i];
			float tmpDistance = Vector3.Distance (tmpPlanet.transform.position, player.transform.position);
			if(tmpDistance > targetDistance)
			{
				targetPlanet = tmpPlanet;
			}
		}
		targetGravity = planets[1].GetComponent<GravityAttractor>();

		targetGravity.Attract (transform);
	}
}
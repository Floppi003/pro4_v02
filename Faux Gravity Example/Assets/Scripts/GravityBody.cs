using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	GravityAttractor targetGravity;
	GameObject player;
	GameObject[] planets;
	GameObject targetPlanet;

	void Awake () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		player = GameObject.FindGameObjectWithTag("Player");
		targetPlanet = planets[0];
		//planets = GameObject.FindGameObjectsWithTag("Planet").GetComponent<GravityAttractor>();
		//planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		
		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}

	//FixedUpdate gets called at a regular interval independent from the framerate
	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity

		float targetDistance = Vector3.Distance (targetPlanet.transform.position, player.transform.position);

		for(var i = 1; i < planets.Length; i++)
		{
			GameObject tmpPlanet = planets[i];
			float tmpDistance = Vector3.Distance (tmpPlanet.transform.position, player.transform.position);
			Debug.Log ("TmpDistance: " + tmpDistance);
			if(tmpDistance < targetDistance)
			{
				targetPlanet = tmpPlanet;
			}
		}

		Debug.Log ("Current Gravity: " + targetPlanet.name
		+"  " + planets[0].name + ": " + Vector3.Distance (planets[0].transform.position, player.transform.position)
	  	+"  " + planets[1].name + ": " + Vector3.Distance (planets[1].transform.position, player.transform.position)
	   	+"  " + planets[2].name + ": " + Vector3.Distance (planets[2].transform.position, player.transform.position)
	   	+"  " + planets[3].name + ": " + Vector3.Distance (planets[3].transform.position, player.transform.position));


	targetGravity = targetPlanet.GetComponent<GravityAttractor> ();
	targetGravity.Attract (transform);
	}
}
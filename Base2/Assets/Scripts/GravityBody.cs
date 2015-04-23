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

		/*
		Timer gravityTimer = new Timer ();
		gravityTimer.Interval = (1000); //1 second
		gravityTimer.Tick += new Eventhandler (FindTargetPlanet);
		gravityTimer.Start ();
		*/

		InvokeRepeating("FindTargetPlanet", 2.0f, 0.5f);
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

	targetGravity = targetPlanet.GetComponent<GravityAttractor> ();
	targetGravity.Attract (transform);

		//linear interpolation
		//Zielposition über Zeitraum
	}
}
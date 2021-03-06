﻿using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public GazeAwareComponent gazePosition;

	public float movementSpeed = 5.0f;
	public float rotationSpeed = 5.0f;
	public float verticalRotation = 0.0f;
	public float upDownRange = 60.0f;
	public float pushStrength;

	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
		gazePosition = GetComponent<GazeAwareComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		//gazePosition = GetComponent<GazeAwareComponent>();
		//Debug.Log (gazePosition.Screen.x);
		//Debug.Log (gazePosition.Screen.y);
		if (gazePosition.HasGaze) {
			Debug.Log ("Blick erfasst");

			// Rotation
			//float rotLeftRight = Input.GetAxis ("Mouse X") * rotationSpeed;
			//transform.Rotate (0, rotLeftRight, 0);

			//verticalRotation -= Input.GetAxis ("Mouse Y") * rotationSpeed;
			transform.Rotate (0, 5, 0);

		}
		//Debug.Log("Hello World!");
		//verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		//Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

		Vector3 speed = new Vector3(sideSpeed,0,forwardSpeed);

		speed = transform.rotation * speed;

		CharacterController cc = GetComponent<CharacterController>();

		cc.SimpleMove(speed);
	}


	void OnControllerColliderHit(ControllerColliderHit hit) {
		Debug.Log ("onControllerColliderHit");

		//gameObject.renderer.material.color = new Color (0.0f, 1.0f, 0.0f);

		Rigidbody body = hit.collider.attachedRigidbody;
		
		if (body == null || body.isKinematic) {
			Debug.Log ("Sphere is null/kinematic");
			return;
		}
		
		if (hit.moveDirection.y < -0.3f) {
			Debug.Log ("Sphere moveDirection < -0.3f");
			return;
		}
		
		pushStrength = movementSpeed / 1.6f;
		
		Vector3 direction = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = direction * pushStrength;

		// change color
		GetComponent<Renderer>().material.color = Color.red;

		//cc.Move (movementSpeed*Time.deltaTime);
	}
}
using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float mouseSensivity = 5.0f;
	public float verticalRotation = 0.0f;
	public float upDownRange = 60.0f;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensivity;
		transform.Rotate(0, rotLeftRight, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensivity;
		//Debug.Log("Hello World!");
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

		Vector3 speed = new Vector3(sideSpeed,0,forwardSpeed);

		speed = transform.rotation * speed;

		CharacterController cc = GetComponent<CharacterController>();

		cc.SimpleMove(speed);
	
	}
}

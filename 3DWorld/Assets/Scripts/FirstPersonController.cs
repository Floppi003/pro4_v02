using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float mouseSensivity = 5.0f;
	public float jumpSpeed = 5.0f;

	float verticalRotation = 0.0f;
	public float upDownRange = 60.0f;
	public float pushStrength;

	float verticalVelocity = 0;

	CharacterController cc;

	bool normalGravity = true;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		cc = GetComponent<CharacterController>();
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

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
		}

		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		
		speed = transform.rotation * speed;

		cc.Move(speed * Time.deltaTime);

		// Inverted Gravity - currently unused
		if (Input.GetKeyDown (KeyCode.G)) {
			normalGravity = !normalGravity;
		}

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

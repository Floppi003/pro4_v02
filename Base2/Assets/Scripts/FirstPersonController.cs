using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float walkSpeed = 8; //movement/walking speed
	public float jumpForce = 300; //jump height/strength
	public LayerMask groundedMask; //mask for raytracing/jumping - reference plane for the raycast
	
	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	
	
	void Awake() {
		Screen.lockCursor = true;
		cameraTransform = Camera.main.transform;
	}
	
	void Update() {
		
		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		
		Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f); //ref allows to modify a global variable
		
		// Jump
		if (Input.GetButtonDown("Jump")) {
			Debug.Log("Jump!");
			if (IsGrounded()) {
				GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
				Debug.Log("Grounded!");
			}
		}
	}
	
	bool IsGrounded ()
	{
		//Physics.Raycast(ray, out hit, 1 + .2f, groundedMask
		return (Physics.Raycast (transform.position, - transform.up, 1 + 0.1f)); //letzter Parameter groundedMask
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
	}
}
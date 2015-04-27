using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float walkSpeed = 8; //movement/walking speed
	public float jumpForce = 300; //jump height/strength
	public float jumpDamping = 0; // reduced movement while jumping
	public LayerMask groundedMask; //mask for raytracing/jumping - reference plane for the raycast#


	// audio files
	public AudioClip greenClip1;
	public AudioClip greenClip2;
	public AudioClip greenClip3;
	public AudioClip redClip1;
	public AudioClip redClip2;
	public AudioClip blueClip1;
	public AudioClip blueClip2;

	private float timeSinceLastButtonAudioPlay = 0.0f;


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

		timeSinceLastButtonAudioPlay += Time.deltaTime;
		// set dampig dependend if grounded or not
		float damping;
		if (IsGrounded()) {
			damping = 1;
		} else {
			damping = jumpDamping;
		}
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

		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f * damping); //ref allows to modify a global variable

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


	public void playGreenSound() {
		if (timeSinceLastButtonAudioPlay < 3.0f) {
			return;
		}

		timeSinceLastButtonAudioPlay = 0.0f;


		Debug.Log ("play Green sound called");
		float random = Random.Range (0.0f, 3.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.greenClip1);
		} else if (random < 2.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.greenClip2);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (this.greenClip3);
		}
	}

	public void playRedSound() {
		if (timeSinceLastButtonAudioPlay < 3.0f) {
			return;
		}
		
		timeSinceLastButtonAudioPlay = 0.0f;


		Debug.Log ("play Red sound called");
		float random = Random.Range (0.0f, 2.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.redClip1);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (this.redClip2);
		}
	}

	public void playBlueSound() {
		if (timeSinceLastButtonAudioPlay < 3.0f) {
			return;
		}
		
		timeSinceLastButtonAudioPlay = 0.0f;


		Debug.Log ("play Blue sound called");
		float random = Random.Range (0.0f, 2.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.blueClip1);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (this.blueClip2);
		}
	}
}


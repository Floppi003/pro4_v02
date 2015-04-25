//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Component that encapsulates a provider for <see cref="EyeXEyePosition"/> data.
/// </summary>
[AddComponentMenu("Tobii EyeX/Eye Position Data")]
public class EyePositionDataComponent : MonoBehaviour
{
    public FixationDataMode fixationDataMode;

    private EyeXHost _eyexHost;
    private IEyeXDataProvider<EyeXEyePosition> _dataProvider;
	private int eyesClosedCounter;
	private int leftEyeOpenedCounter;
	private int rightEyeOpenedCounter;

    /// <summary>
    /// Gets the last eye position.
    /// </summary>
    public EyeXEyePosition LastEyePosition { get; private set; }

    protected void Awake()
    {
		eyesClosedCounter = 0;
        _eyexHost = EyeXHost.GetInstance();
        _dataProvider = _eyexHost.GetEyePositionDataProvider();
    }

    protected void OnEnable()
    {
        _dataProvider.Start();
    }

    protected void OnDisable()
    {
        _dataProvider.Stop();
    }

    protected void Update()
    {
        LastEyePosition = _dataProvider.Last;

		// Get the last eye position.
		EyeXEyePosition lastEyePosition = GetComponent<EyePositionDataComponent>().LastEyePosition;

		if (lastEyePosition.IsValid) {
			// Get the position of the left eye.
			Vector3 leftEyePosition = new Vector3 (lastEyePosition.LeftEye.X,
			                                       lastEyePosition.LeftEye.Y,
			                                       lastEyePosition.LeftEye.Z);
			
			
			// The Eye Position of a closed eye will be (0, 0, 0)
			// Sometimes single eye positions can be (0, 0, 0) even though the eye was opened
			
			Vector3 rightEyePosition = new Vector3(lastEyePosition.RightEye.X,
			                                       lastEyePosition.RightEye.Y,
			                                       lastEyePosition.RightEye.Z);
			
			
			//Debug.Log ("leftEyePosition: " + leftEyePosition);
			//Debug.Log ("rightEyePosition: " + rightEyePosition);


			if (leftEyePosition.Equals (new Vector3(0, 0, 0)) && rightEyePosition.Equals (new Vector3(0, 0, 0))) {
				// Eyes closed
				eyesClosedCounter++;
				//Debug.Log ("!!! - !!! both eyes closed!!!!!!!!");

			} else if (leftEyePosition.Equals (new Vector3(0, 0, 0)) && !rightEyePosition.Equals (new Vector3(0, 0, 0))) {
				// only left eye is opened
				this.leftEyeOpenedCounter++;
				this.rightEyeOpenedCounter = this.rightEyeOpenedCounter / 3;
				this.hideRightEyeObjects();
				this.showLeftEyeObjects();


			} else if (!leftEyePosition.Equals (new Vector3(0, 0, 0)) && rightEyePosition.Equals (new Vector3(0, 0, 0))) {
				// only right eye is opened
				this.rightEyeOpenedCounter++;
				this.leftEyeOpenedCounter = this.leftEyeOpenedCounter / 3;
				this.hideLeftEyeObjects ();
				this.showRightEyeObjects ();

			} else {
				// both eyes opened
				eyesClosedCounter = eyesClosedCounter / 3;

				this.leftEyeOpenedCounter = this.leftEyeOpenedCounter / 2;
				this.rightEyeOpenedCounter = this.rightEyeOpenedCounter / 2;
				this.hideLeftEyeObjects();
				this.hideRightEyeObjects ();
			}

			/*
			if ((eyesClosedCounter > leftEyeOpenedCounter && eyesClosedCounter > rightEyeOpenedCounter) || 
			    (eyesClosedCounter < 10 && leftEyeOpenedCounter < 10 && rightEyeOpenedCounter < 10)) {

				// don't show anything
				this.hideLeftEyeObjects();
				this.hideRightEyeObjects();

			} else if (leftEyeOpenedCounter > rightEyeOpenedCounter) {
				// show only left objects
				this.showLeftEyeObjects();
				this.hideRightEyeObjects();

			} else {
				// show only right objects
				this.showRightEyeObjects();
				this.hideLeftEyeObjects();
			}
*/



			// check whether bridge should be broad or not
			// make bridge wider if both eyes are closed
			GameObject bridge = GameObject.Find ("bridge");

			if (eyesClosedCounter > 24) {
				bridge.transform.localScale = new Vector3(1, 1, 6);
				GetComponent<BridgeCollider>().enabled = false;
			} else {
				bridge.transform.localScale = new Vector3(1, 1, 0.1f);
				GetComponent<BridgeCollider>().enabled = true;
			}
			
		} else {
			//Debug.Log ("leftEyePosition INVALID!");
		}
    }


	private void showLeftEyeObjects() {
		GameObject[] leftEyeObjects = GameObject.FindGameObjectsWithTag ("LeftEye");
		
		foreach (GameObject leftEyeObject in leftEyeObjects) {
			leftEyeObject.GetComponent<MeshCollider>().enabled = true;
			leftEyeObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	private void showRightEyeObjects() {
		GameObject[] rightEyeObjects = GameObject.FindGameObjectsWithTag ("RightEye");
		
		foreach (GameObject rightEyeObject in rightEyeObjects) {
			rightEyeObject.GetComponent<MeshCollider>().enabled = true;
			rightEyeObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	private void hideLeftEyeObjects() {
		GameObject[] leftEyeObjects = GameObject.FindGameObjectsWithTag ("LeftEye");
		
		foreach (GameObject leftEyeObject in leftEyeObjects) {
			leftEyeObject.GetComponent<MeshCollider>().enabled = false;
			leftEyeObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private void hideRightEyeObjects() {
		GameObject[] rightEyeObjects = GameObject.FindGameObjectsWithTag ("RightEye");
		
		foreach (GameObject rightEyeObject in rightEyeObjects) {
			rightEyeObject.GetComponent<MeshCollider>().enabled = false;
			rightEyeObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

}

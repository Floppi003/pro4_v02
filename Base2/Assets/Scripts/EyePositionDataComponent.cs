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

    /// <summary>
    /// Gets the last eye position.
    /// </summary>
    public EyeXEyePosition LastEyePosition { get; private set; }

    protected void Awake()
    {
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


			// make bridge wider if both eyes are closed
			GameObject bridge = GameObject.Find ("bridge");

			if (leftEyePosition.Equals (new Vector3(0, 0, 0)) && rightEyePosition.Equals (new Vector3(0, 0, 0))) {
				//Debug.Log ("!!! - !!! both eyes closed!!!!!!!!");
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
}

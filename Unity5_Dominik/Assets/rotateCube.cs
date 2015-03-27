using UnityEngine;
using System.Collections;

public class rotateCube : MonoBehaviour {
	private GazeAwareComponent _gazeAware;

	// Use this for initialization
	void Start () {
		_gazeAware = GetComponent<GazeAwareComponent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_gazeAware.HasGaze) {
			transform.Rotate (Vector3.forward);
		}
	}
}

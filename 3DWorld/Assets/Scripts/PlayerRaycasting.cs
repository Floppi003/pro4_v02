using UnityEngine;
using System.Collections;

public class PlayerRaycasting : MonoBehaviour {

	public float distanceToSee;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.magenta);
		Camera.main.ScreenPointToRay(new Vector3(0.0f, 0.0f, 0.0f));
	}
}

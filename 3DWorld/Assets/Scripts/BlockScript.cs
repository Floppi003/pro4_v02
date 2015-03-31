using UnityEngine;
using System.Collections;
using Tobii.EyeX.Framework;


public class BlockScript : MonoBehaviour {

	public GazePointDataMode gazePointDataMode = GazePointDataMode.LightlyFiltered;
	
	private EyeXHost _eyexHost;
	private IEyeXDataProvider<EyeXGazePoint> _dataProvider;
	
	/// <summary>
	/// Gets the last gaze point.
	/// </summary>
	public EyeXGazePoint LastGazePoint { get; private set; }
	
	protected void Awake()
	{
		_eyexHost = EyeXHost.GetInstance();
		_dataProvider = _eyexHost.GetGazePointDataProvider(gazePointDataMode);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Block Update()");
		EyeXGazePoint LastGazePoint = LastGazePoint = _dataProvider.Last;

		if (LastGazePoint.IsValid) {

			Vector2 screenCoordinates = LastGazePoint.Screen;
			Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(screenCoordinates.x, screenCoordinates.y, 0));
			Debug.Log ("Screen Coordinates: " + screenCoordinates);
			Debug.Log ("Last Gaze Point: " + LastGazePoint);
			Debug.Log ("World Coordinates: " + worldCoordinates);



			//transform.position = new Vector3(screenCoordinates.x, screenCoordinates.y, 0);

			/*Vector3 assignVector = new Vector3(worldCoordinates.x, worldCoordinates.y, 0.0f);
			transform.position = assignVector;
			Debug.Log ("Transform: " + transform.position);*/
			
				/*
			uWorldGazePoints.Enqueue(new Vector3(worldCoordinates.x, worldCoordinates.y, 0.0f));
			
			if (uWorldGazePoints.Count > 10) {
				
				float x = 0;
				float y = 0;
				float z = 0;
				
				foreach(Vector3 v in uWorldGazePoints) {
					x = x + v.x;
					y = y + v.y;
					z = z + v.z;
				}
				
				x = x / uWorldGazePoints.Count;
				y = y / uWorldGazePoints.Count;
				z = z / uWorldGazePoints.Count;
				
				uWorldGazePoints.Dequeue();
				
				transform.position = new Vector3(x, y, z);
			}
			*/
		}


		Vector3 playerDirection = GameObject.Find ("MainCamera").transform.rotation.eulerAngles;
		Vector3 playerPosition = GameObject.Find("MainCamera").transform.position;
		Debug.Log ("player Direction... x: " + playerDirection.x + ", y: " + playerDirection.y + ", z: " + playerDirection.z);
		Debug.Log ("player Position... x: " + playerPosition.x + ", y: " + playerPosition.y + ", z: " + playerPosition.z);
		
		float rayDistance = 10.0f;
		Debug.DrawRay (playerPosition, new Vector3(0.0f, 0.5f, 1.0f) * 100000.0f);
		
		RaycastHit hit;
		if (Physics.Raycast(playerPosition, playerDirection, out hit, rayDistance)) {
			float distance = hit.distance;
			Debug.Log ("ray cast collided with distance: " + distance);
		}
}

//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Component that encapsulates a provider for <see cref="EyeXGazePoint"/> data.
/// </summary>
[AddComponentMenu("Tobii EyeX/Gaze Point Data")]
public class GazePointDataComponent : MonoBehaviour
{
    public GazePointDataMode gazePointDataMode = GazePointDataMode.LightlyFiltered;
	public float distanceToSee;

    private EyeXHost eyexHost;
    private IEyeXDataProvider<EyeXGazePoint> dataProvider;
	private RaycastHit gazeRaycastHit;

    /// <summary>
    /// Gets the last gaze point.
    /// </summary>
    public EyeXGazePoint lastGazePoint { get; private set; }

    protected void Awake()
    {
        eyexHost = EyeXHost.GetInstance();
        dataProvider = eyexHost.GetGazePointDataProvider(gazePointDataMode);
    }

    protected void OnEnable()
    {
        dataProvider.Start();
    }

    protected void OnDisable()
    {
        dataProvider.Stop();
    }

    protected void Update()
    {
        lastGazePoint = dataProvider.Last;
		
		if (lastGazePoint.IsValid) {

			Vector2 screenCoordinates = lastGazePoint.Screen;
			Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(screenCoordinates.x, screenCoordinates.y, 0));

			//Debug.Log ("Last Gaze Point: " + lastGazePoint);
			//Debug.Log ("Screen Coordinates: " + screenCoordinates);
			//Debug.Log ("World Coordinates: " + worldCoordinates);


			Ray gazeRay = Camera.main.ScreenPointToRay(new Vector3(screenCoordinates.x, screenCoordinates.y, 0.0f));
			Debug.DrawRay (gazeRay.origin, gazeRay.direction * distanceToSee, Color.magenta);

			if (Physics.Raycast (gazeRay.origin, gazeRay.direction, out gazeRaycastHit, distanceToSee)) {
				Debug.Log ("I gazed: " + gazeRaycastHit.collider.gameObject.name);
				gazeRaycastHit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;

				// set timer of block to 1 (when timer reaches 0 it will get standard gray color)
				BlockScript blockScript = gazeRaycastHit.collider.gameObject.GetComponent<BlockScript>();
				blockScript.lostGazeTimer = 0.35f;
			}

			/*uWorldGazePoints.Enqueue(new Vector3(worldCoordinates.x, worldCoordinates.y, 0.0f));
			
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
    }
}

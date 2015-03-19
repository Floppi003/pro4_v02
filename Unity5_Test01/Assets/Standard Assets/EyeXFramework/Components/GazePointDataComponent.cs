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
		LastGazePoint = GetComponent<GazePointDataComponent> ().LastGazePoint;
		LastGazePoint = _dataProvider.Last;

		if (LastGazePoint.IsValid) {
			Debug.Log ("Last Gaze Point: " + LastGazePoint);

			Vector2 screenCoordinates = LastGazePoint.Screen;

			Debug.Log ("Screen Coordinates: " + screenCoordinates);

			Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(screenCoordinates.x, screenCoordinates.y, 0));
			Debug.Log ("World Coordinates: " + worldCoordinates);

			//transform.position = new Vector3(screenCoordinates.x, screenCoordinates.y, 0);

			Vector3 assignVector = new Vector3(worldCoordinates.x, worldCoordinates.y, 0.0f);
			transform.position = assignVector;
			Debug.Log ("Transform: " + transform.position);

		}
    }
}

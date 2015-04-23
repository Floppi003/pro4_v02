using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	public int levelToLoad;
	public GameObject padlock;
	private string loadPrompt;
	private bool inRange;
	private int completedLevel;
	private bool canLoadLevel;

	void Start()
	{
		completedLevel = PlayerPrefs.GetInt ("Level Completed");
		if (completedLevel == 0) 
		{
			completedLevel = 1;
		}
		canLoadLevel = levelToLoad <= completedLevel ? true : false;
		if(!canLoadLevel)
		{
			Instantiate (padlock, new Vector3(transform.position.x + 2f, 0f, transform.position.z), Quaternion.identity);
		}
	}

	void Update()
	{
		if (canLoadLevel && inRange && Input.GetButtonDown("Action")) 
		{
			Application.LoadLevel ("Level " + levelToLoad.ToString ());
		}
	}

	void OnTriggerStay(Collider other)
	{
		inRange = true;
		if (canLoadLevel) {
			loadPrompt = "Press [I] to load level " + levelToLoad.ToString ();
		} else {
			loadPrompt = "Level " + levelToLoad.ToString () + " is locked";
		}

	}

	void OnTriggerExit()
	{
		loadPrompt = "";
		inRange = false;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (30, Screen.height * .9f, 200, 40), loadPrompt);
	}
}

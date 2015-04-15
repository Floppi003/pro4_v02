using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Count
	public int maxLevels = 3; //max id = maxLevels - 1
	public int currentScore;
	public int highscore;
	public int tokenCount;
	private int totalTokenCount;
	public int currentLevel = 1; //start with 1 = id 0
	public int unlockedLevel = 1; //start with 1 = id 0

	// Timer variables
	public Rect timerRect;
	public Color warningColorTimer;
	public Color defaultColorTimer;
	public float startTime;
	private string currentTime;

	// GUI SKI
	public GUISkin skin;

	// References
	public GameObject tokenParent;

	private bool completed = false;
	private bool showWinScreen = false;
	public int winScreenWidth, winScreenHeight;

	void Update()
	{
		if (!completed)
		{
			startTime -= Time.deltaTime;
			currentTime = string.Format("{0:0.0}", startTime);
			if (startTime <= 0)
			{
				startTime = 0;
				//Application.LoadLevel("main_menu");
			}
		}
	}

	void Start()
	{
		totalTokenCount = tokenParent.transform.childCount;

		if (PlayerPrefs.GetInt("Level Unlocked") > 1) //if there are more levels unlocked than level 1, let him play them
		{
			unlockedLevel = PlayerPrefs.GetInt("Level Unlocked");
		} else {
			unlockedLevel = 1;
		}

		if (PlayerPrefs.GetInt("Current Level") > 1) //if there are more levels unlocked than level 1, let him play them
		{
			currentLevel = PlayerPrefs.GetInt ("Current Level");
		} else {
			currentLevel = 1;
		}
	}
	
	public void CompleteLevel()
	{
		showWinScreen = true;
		completed = true;
	}

	void LoadNextLevel()
	{
		Time.timeScale = 1f;
		if (currentLevel < maxLevels) //current level id (-1) < max level id (-1)
		{
			print ("currentLevel before: " + currentLevel);
			currentLevel += 1;
			print ("currentLevel after: " + currentLevel);
			print (currentLevel);
			SaveGame();
			//Application.LoadLevel ("Level " + currentLevel.ToString ()); //load by name instead of id
			int nextLevel = currentLevel-1;
			Application.LoadLevel("Level " + currentLevel); //level name
			//DontDestroyOnLoad() - don't reset value with new scene
		} else {
			Application.LoadLevel("main_menu");
			print ("You win!");
		}
	}

	void SaveGame()
	{
		if (unlockedLevel < currentLevel) {
			unlockedLevel = currentLevel;
			PlayerPrefs.SetInt ("Level Unlocked", unlockedLevel);
			PlayerPrefs.SetInt ("Current Level", currentLevel);
		}
		PlayerPrefs.SetInt("Level: " + currentLevel.ToString() + " Score: ", currentScore);
	}

	void OnGUI()
	{
		GUI.skin = skin;
		if (startTime < 5f)
		{
			skin.GetStyle("Timer").normal.textColor = warningColorTimer;
		} else {
			skin.GetStyle("Timer").normal.textColor = defaultColorTimer;
		}
		GUI.Label (timerRect, currentTime, skin.GetStyle ("Timer"));
		GUI.Label (new Rect(45,100,200,200), tokenCount.ToString() + "/" + totalTokenCount.ToString());

		if (showWinScreen)
		{
			Rect winScreenRect = new Rect(Screen.width/2 - (Screen.width *.5f/2), Screen.height/2 - (Screen.height *.5f/2), Screen.width *.5f, Screen.height *.5f);
			GUI.Box(winScreenRect, "Yeah");

			int gameTime = (int)startTime;
			currentScore = tokenCount * gameTime;
			if (GUI.Button(new Rect(winScreenRect.x + winScreenRect.width - 170, winScreenRect.y + winScreenRect.height - 60, 150, 40), "Continue"))
			{
				LoadNextLevel();
			}
			if (GUI.Button(new Rect(winScreenRect.x + 20, winScreenRect.y + winScreenRect.height - 60, 100, 40), "Quit"))
			{
				currentLevel += 1;
				SaveGame ();
				Application.LoadLevel("main_menu");
				Time.timeScale = 1f;
			}

			GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 40, 300, 50), currentScore.ToString() + " Score");
			GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 70, 300, 50), "Completed Level " + currentLevel);
		}
	}
}








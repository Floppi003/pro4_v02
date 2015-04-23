using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI (){
		GUI.Label (new Rect(10,10,400, 45), "Paradoxon");
		if(GUI.Button (new Rect(10,150,100,45), "Play")){
			Application.LoadLevel(0); //Index
		}
		if(GUI.Button (new Rect(10,205,100,45), "Quit")){
			Application.Quit();
		}
	}
}

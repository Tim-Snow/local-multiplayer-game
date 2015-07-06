using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	public static bool paused = false;
	GameObject menu;

	public void BackToMenu(){
		Application.LoadLevel ("MainMenu");
	}
	
	public void togglePause(){
		if (paused) {
			paused = false;
			Time.timeScale = 1f;
			menu.SetActive(false);
		} else {
			paused = true;
			Time.timeScale = 0f;
			menu.SetActive (true);
		}
	}

	public void Resume(){
		Time.timeScale = 1f;
		menu.SetActive(false);
	}

	void Start(){
		menu = GameObject.Find("PauseMenu");
		menu.SetActive (false);
	}

	void Update(){
		if (Input.GetButtonDown ("Start") || Input.GetKeyDown(KeyCode.Escape))
			togglePause ();
	}
}

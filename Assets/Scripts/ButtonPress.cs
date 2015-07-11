using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {

	public void BackToMenu(){
		Time.timeScale = 1f;
		Application.LoadLevel ("MainMenu");
	}


	void Start(){

	}

	void Update(){

	}
}

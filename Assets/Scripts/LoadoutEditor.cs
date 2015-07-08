using UnityEngine;
using System.Collections;

public class LoadoutEditor : MonoBehaviour {

	void Start () {
	
	}

	public void goBack(){
		Application.LoadLevel ("MainMenu");
	}

	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			goBack ();
		}
	}
}

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string charSelect;
	public string loadoutEditor;
	public string optionMenu;
	public string statMenu;

	public void NewGame(){
		Application.LoadLevel (charSelect);
	}

	public void Loadouts(){
		Application.LoadLevel (loadoutEditor);
	}

	public void Options(){
		Application.LoadLevel (optionMenu);
	}

	public void Stats(){
		Application.LoadLevel (statMenu);
	}

	public void Exit(){
		Application.Quit ();
	}

}

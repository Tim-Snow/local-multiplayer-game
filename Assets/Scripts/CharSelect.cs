using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharSelect : MonoBehaviour {
	bool p1 = false;
	bool p2 = false;
	bool p3 = false;
	bool p4 = false;
	Image  PanelP1;
	Image  PanelP2;
	Image  PanelP3;
	Image  PanelP4;
	GameObject  StartPanel;
	int numPlayers = 0;
	
	public static Dictionary<int, bool> enteredPlayers;//change to int and custom object containing player info, eg colour, class

	void Update () {
		if (Input.GetButtonDown ("A_P1")) 
			addPlayer(1, ref p1, ref PanelP1);

		if (Input.GetButtonDown ("A_P2"))
			addPlayer(2, ref p2, ref PanelP2);

		if (Input.GetButtonDown ("A_P3"))
			addPlayer(3, ref p3, ref PanelP3);

		if (Input.GetButtonDown ("A_P4"))
			addPlayer(4, ref p4, ref PanelP4);

		if(Input.GetButtonDown("B_P1"))
			removePlayer(1, ref p1, ref PanelP1);
		
		if(Input.GetButtonDown("B_P2"))
			removePlayer(2, ref p2, ref PanelP2);
		
		if(Input.GetButtonDown("B_P3"))
			removePlayer(3, ref p3, ref PanelP3);
		
		if(Input.GetButtonDown("B_P4"))
			removePlayer(4, ref p4, ref PanelP4);

		if (numPlayers < 1)	StartPanel.SetActive (false);
		

		if (numPlayers >= 1) {
			StartPanel.SetActive (true);

			if(Input.GetButtonDown("Start"))
				Application.LoadLevel("Game");

			for(int i = 1; i < 5; i++){
				if(Input.GetButtonDown("B_P" + i)){
					if(enteredPlayers[i])
						Application.LoadLevel("Game");
				}
			}
		}
	}

	public void addPlayer(int pNum, ref bool flag, ref  Image panel){
		if (!flag) {
			numPlayers++;
			enteredPlayers [pNum] = true;
		}
		panel.color = UnityEngine.Color.green;
		flag = true;
	}

	public void removePlayer(int pNum, ref bool flag, ref Image panel){
		if(flag){
			panel.color = UnityEngine.Color.white;
			flag = false;
			numPlayers--;
			enteredPlayers [pNum] = false;
		} else { Application.LoadLevel ("MainMenu"); }
	}

	void Start(){
		enteredPlayers = new Dictionary<int, bool>();
		for (int i = 1; i < 5; i++) {
			enteredPlayers[i] = false;
		}
		PanelP1 	= GameObject.Find("Panel"  ).GetComponent<Image>();
		PanelP2 	= GameObject.Find("Panel 1").GetComponent<Image>();
		PanelP3 	= GameObject.Find("Panel 2").GetComponent<Image>();
		PanelP4 	= GameObject.Find("Panel 3").GetComponent<Image>();
		StartPanel  = GameObject.Find("Start Panel");

		addPlayer(2, ref p2, ref PanelP2);
		addPlayer(3, ref p3, ref PanelP3);
		addPlayer(4, ref p4, ref PanelP4);
		StartPanel.SetActive (false);
	}
}
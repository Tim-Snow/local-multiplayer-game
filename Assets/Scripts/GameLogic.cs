using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public static bool paused;

	public Text 	endRoundText;
	static bool[] 	alivePlayers;
	static int 		numPlayersAlive;

	GameObject[] 	players;
	GameObject 		roundUI;
	GameObject 		menu;

	int 			addPlayerIter;
	bool 			roundOver;
	float 			timeSinceRoundEnded;
	bool 			endScreenShowing;

	public static void playerDied(int playerNum){
		numPlayersAlive -= 1;
		alivePlayers[playerNum - 1] = false;
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

	void Start () {
		menu = GameObject.Find("PauseMenu");
		menu.SetActive (false);
		paused = false;

		paused = false;
		timeSinceRoundEnded = 0f;
		endScreenShowing 	= false;
		roundOver 			= false;
		addPlayerIter 		= 0;
		numPlayersAlive 	= 0;
		alivePlayers 		= new bool[CharSelect.enteredPlayers.Count];
		roundUI 			= GameObject.Find ("EndRoundUI");

		roundUI.SetActive(false);

		for (int i = 1; i <= CharSelect.enteredPlayers.Count; i++) {
			if(CharSelect.enteredPlayers[i].isEntered){
				alivePlayers[i-1] = true;
				numPlayersAlive++;
			} else {
				alivePlayers[i-1] = false;
			}
		}

		players = new GameObject[numPlayersAlive];

		for (int i = 1; i <= CharSelect.enteredPlayers.Count; i++) {
			if (CharSelect.enteredPlayers[i].isEntered) {
				enteredPlayerDetails thePlayer = CharSelect.enteredPlayers[i];
				var spawnPoint = new Vector2 (2 + i * 2, 0);

				if(thePlayer.selectedClass == 0){//ARCHER
					players[addPlayerIter] = Instantiate (Resources.Load ("Archer", typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
				} else {
					players[addPlayerIter] = Instantiate (Resources.Load ("Player", typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
				}

				Player playerScript = players[addPlayerIter].GetComponent<Player> ();
				playerScript.joystickNumber 			= i;
				playerScript.timeToJumpApex 			= 0.3f;
				playerScript.jumpHeight 				= 2;		
				playerScript.accelerationTimeAirborne 	= .2f;
				playerScript.accelerationTimeGrounded 	= .1f;
				playerScript.moveSpeed 					= 6;

				players[addPlayerIter].GetComponent<Renderer>().material.color = thePlayer.selectedColor;

				Controller controllerScript = players[addPlayerIter].GetComponent<Controller> ();
				controllerScript.collider 	= players[addPlayerIter].AddComponent<BoxCollider2D> ();

				controllerScript.collisionMask 			= 1 << 9;
				controllerScript.horizontalRayCount 	= 4;
				controllerScript.verticalRayCount 		= 4;
				addPlayerIter++;
			}
		}

		Time.timeScale = 1f;
	}
	
	void Update () {
		if (!roundOver) {
			if (Input.GetButtonDown ("Start") || Input.GetKeyDown(KeyCode.Escape))
				togglePause ();

			if (numPlayersAlive <= 1)
				roundOver = true;
		} else {
			if(!endScreenShowing){
				if(timeSinceRoundEnded >= 1.5f){
					int winner = 5;

					for (int i = 0; i < alivePlayers.Length; i++) {
						if (alivePlayers [i])	winner = i + 1;
					}

					if(winner == 5)	endRoundText.text = "Everyone died!";
					else			endRoundText.text = "Player " + winner + " wins the round!";

					roundUI.SetActive (true);
					endScreenShowing = true;
				}
			}
			if(timeSinceRoundEnded >= 2.0f){
				if(Input.GetButtonDown("Submit") || Input.GetButtonDown ("Start"))
					Application.LoadLevel("CharSelect");
			} else {
				timeSinceRoundEnded += Time.deltaTime;
			}
		}
	}
}

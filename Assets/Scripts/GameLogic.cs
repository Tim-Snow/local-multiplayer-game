using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	GameObject[] players;

	void Start () {
		players = new GameObject[CharSelect.enteredPlayers.Count];

		for (int i = 1; i <= players.Length; i++) {
			if (CharSelect.enteredPlayers[i].isEntered) {
				enteredPlayerDetails thePlayer = CharSelect.enteredPlayers[i];

				var spawnPoint = new Vector2 (2 + i * 2, 0);

				if(thePlayer.selectedClass == 0){//ARCHER
					players[i] = Instantiate (Resources.Load ("Archer", typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
					Archer archerScript 		= players[i].GetComponent<Archer> ();

					archerScript.joystickNumber 			= i;
					archerScript.timeToJumpApex 			= 0.3f;
					archerScript.jumpHeight 				= 2;		
					archerScript.accelerationTimeAirborne 	= .2f;
					archerScript.accelerationTimeGrounded 	= .1f;
					archerScript.moveSpeed 					= 6;
				} else {
					players[i] = Instantiate (Resources.Load ("Player", typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
					Player playerScript 		= players[i].GetComponent<Player> ();

					playerScript.joystickNumber 			= i;
					playerScript.timeToJumpApex 			= 0.3f;
					playerScript.jumpHeight 				= 2;		
					playerScript.accelerationTimeAirborne 	= .2f;
					playerScript.accelerationTimeGrounded 	= .1f;
					playerScript.moveSpeed 					= 6;
				}

				players[i].GetComponent<Renderer>().material.color = thePlayer.selectedColor;

				Controller controllerScript = players[i].GetComponent<Controller> ();
				controllerScript.collider 	= players[i].AddComponent<BoxCollider2D> ();

				controllerScript.collisionMask 			= 1 << 9;
				controllerScript.horizontalRayCount 	= 4;
				controllerScript.verticalRayCount 		= 4;
			}
		}

		Time.timeScale = 1f;
	}

	void Update () {

	}
}

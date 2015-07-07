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

				players[i] = Instantiate (Resources.Load ("Player", typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
				players[i].GetComponent<Renderer>().material.color = thePlayer.selectedColor;

				Player playerScript 		= players[i].GetComponent<Player> ();
				Controller controllerScript = players[i].GetComponent<Controller> ();
				controllerScript.collider 	= players[i].AddComponent<BoxCollider2D> ();

				playerScript.joystickNumber 			= i;
				playerScript.timeToJumpApex 			= 0.3f;
				playerScript.jumpHeight 				= 2;		
				playerScript.accelerationTimeAirborne 	= .2f;
				playerScript.accelerationTimeGrounded 	= .1f;
				playerScript.moveSpeed 					= 6;

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

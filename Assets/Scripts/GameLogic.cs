using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	void Start () {
		for (int i = 1; i <= CharSelect.enteredPlayers.Count; i++) {
			if (CharSelect.enteredPlayers [i] == true) {
				var spawnPoint = new Vector2 (2 + i * 2, 0);

				GameObject player 					= Instantiate (Resources.Load ("Player", typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;

				if(i == 2)
					player.GetComponent<Renderer>().material.color = Color.magenta;

				if(i == 3)
					player.GetComponent<Renderer>().material.color = Color.yellow;

				if(i == 4)
					player.GetComponent<Renderer>().material.color = Color.red;

				Player playerScript 				= player.GetComponent<Player> ();
				Controller controllerScript 		= player.GetComponent<Controller> ();
				controllerScript.collider 			= player.AddComponent<BoxCollider2D> ();

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
		for (int i = 1; i <= CharSelect.enteredPlayers.Count; i++) {//change to actual palyer objects

		}
	}
}

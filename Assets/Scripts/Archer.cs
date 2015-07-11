using UnityEngine;
using System;

public class Archer : Player {
	public GameObject 	bow;
	BowController 		bowScript;

	void Start(){
		base.Start ();
		bowScript = bow.GetComponent<BowController> ();
	}

	void Update(){
		if (!GameLogic.paused) {
			base.Update();

			Vector3 arrowInput = new Vector3 (Input.GetAxis ("RightJoystickX_P" + base.joystickNumber.ToString ()), -Input.GetAxis ("RightJoystickY_P" + base.joystickNumber.ToString ()), 0);
			if(!isHit){
				bowScript.joyInput = arrowInput;
				bowScript.velocity = velocity;
			}
		}
	}
}

using UnityEngine;
using System;

public class Archer : Player {
	public GameObject 	bow;
	BowController 		bowScript;

	int numJumpsRem = 0;
	int numJumps = 2;
	float 		jumpVelocity;
	float 		velocityXSmoothing;

	public int 	joystickNumber;
	string 		joystickString;
	Controller 	controller;

	public void Start(){
		bowScript = bow.GetComponent<BowController> ();
		controller = GetComponent<Controller> ();
		joystickString = joystickNumber.ToString();
		controller = GetComponent<Controller> ();
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	public void Update(){
		if (!ButtonPress.paused) {
			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}
			
			Vector2 input = new Vector2 (Input.GetAxis ("LeftJoystickX_P" + joystickString), 0);
			float targetVelocityX = input.x * moveSpeed;
			
			if(!isHit){
				if (canJump ()) {
					if (Input.GetButtonDown ("A_P" + joystickString)) {
						numJumpsRem--;
						velocity.y = jumpVelocity;
					}
				}
			}
			
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			
			controller.Move (velocity * Time.deltaTime);

			Vector3 arrowInput = new Vector3 (Input.GetAxis ("RightJoystickX_P" + joystickString), -Input.GetAxis ("RightJoystickY_P" + joystickString), 0);
			if(!isHit){
				bowScript.joyInput = arrowInput;
				bowScript.velocity = velocity;
			}
		}
	}

	bool canJump(){
		bool rtn;
		
		if (controller.collisions.below)
			numJumpsRem = numJumps;
		
		if (numJumpsRem == 0) 	rtn = false;
		else 					rtn = true;
		
		return rtn;
	}
}

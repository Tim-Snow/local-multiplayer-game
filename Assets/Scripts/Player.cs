using UnityEngine;
using System;

[RequireComponent (typeof (Controller))]
public class Player : MonoBehaviour {
	
	int numJumpsRem = 0;
	int numJumps = 2;
	
	public float jumpHeight = 2.5f;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
	public float gravity;
	public int joystickNumber;
	public bool isHit = false;

	string 		joystickString;
	float 		jumpVelocity;
	public Vector3 	velocity;
	float 		velocityXSmoothing;
	Controller 	controller;

	public void Start() {
		controller = GetComponent<Controller> ();
		joystickString = joystickNumber.ToString();
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		jumpHeight = 2.5f;
		timeToJumpApex = .4f;
		accelerationTimeAirborne = .2f;
		accelerationTimeGrounded = .1f;
		moveSpeed = 6;
		isHit = false;
		numJumpsRem = 0;
		numJumps = 2;
	}

	public void Update() {
		if (!GameLogic.paused) {
			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}
			
			Vector2 input = new Vector2 (Input.GetAxis ("LeftJoystickX_P" + joystickString), 0);

			float targetVelocityX;
			if(!isHit){
				targetVelocityX = input.x * moveSpeed;
				if (canJump ()) {
					if (Input.GetButtonDown ("A_P" + joystickString)) {
						numJumpsRem--;
						velocity.y = jumpVelocity;
					}
				}
			} else {
				targetVelocityX = 0;
			}

			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;

			controller.Move (velocity * Time.deltaTime);
		}
	}

	protected bool canJump(){
		bool rtn;
		
		if (controller.collisions.below)
			numJumpsRem = numJumps;
		
		if (numJumpsRem == 0) 	rtn = false;
		else 					rtn = true;
		
		return rtn;
	}
}

using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
	public Vector3 		firedAngle;
	public Vector3 		velocity;
	public bool 		isShot;
	public LayerMask 	collisionMask;
	public Collider2D 	collider;
	
	float 				gravity;
	Vector3 			direction, stuckOffset;
	Collider2D 			stuckObject;
	bool 				stop, stuckInPlayer;
	
	void Start () {
		gravity 		= 0.01f;
		isShot 			= false;
		stop 			= false;
		stuckInPlayer 	= false;
		velocity 		= new Vector3(0, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (isShot && !stuckInPlayer && !stop) {
			if (coll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
				stuckInPlayer = true;
				stuckObject = coll;
				stuckOffset = transform.position - stuckObject.transform.position;
				coll.gameObject.GetComponent<Player>().velocity =  (velocity * 30);
				coll.gameObject.GetComponent<Player>().isHit 	= true;
			}
		}

		if (coll.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
			stop = true;
	}
	
	void FixedUpdate () {
		if (stuckInPlayer)
			transform.position = stuckObject.transform.position + stuckOffset;

		if (isShot && !stop && !stuckInPlayer) {
			checkDead ();

			//Unrotate - Move - Rotate
			transform.Rotate (new Vector3 (-firedAngle.x, -firedAngle.y, -firedAngle.z));
			transform.Translate (velocity);
			transform.Rotate (new Vector3 (firedAngle.x, firedAngle.y, firedAngle.z));

			//Calc angle of velocity
			direction = (transform.forward + (velocity)) - transform.forward;
			float angleTravelling = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			//Convert between angle systems
			if(angleTravelling > -90f && angleTravelling < 180f)
				angleTravelling += 90f;
			else {
				angleTravelling = angleTravelling + 450;
			}

			//Rotate projectile based on current veleocity
			firedAngle = new Vector3 (firedAngle.x, firedAngle.y, angleTravelling);
			transform.rotation = Quaternion.Euler(0f, 0f, firedAngle.z);

			//Slow over time
			velocity.x  -= velocity.x * 0.01f;

			//Gravity
			velocity.y  -= gravity;
			if(gravity < 0.02f)
				if(velocity.y <= 0f)
					gravity += gravity * 0.01f;
		}
	}

	void checkDead(){
		//max arrows
		//time existed
		//has hit anything
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine (transform.position, transform.position + direction);
	}
}

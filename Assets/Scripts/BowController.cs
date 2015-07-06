using UnityEngine;
using System.Collections;

public class BowController : MonoBehaviour {

	public Transform  	aimPoint;
	public Transform  	defaultRot;
	public GameObject 	arrow;
	public Vector3 		velocity;
	public Vector3 		joyInput;

	float 				power;
	bool 				readyToFire;
	GameObject[] 		arrows;
	Vector3 			direction;
	
	void Start () {
		readyToFire 		= false;
		defaultRot.rotation = Quaternion.LookRotation(transform.forward, direction);
		arrows 				= new GameObject[1];
		power 				= 1f;
	}

	Vector3 prevBowTransform;
	void Update () {
		float posInX = Mathf.Abs(joyInput.x);
		float posInY = Mathf.Abs(joyInput.y);
		
		bool joyPulledBack = (joyInput.x >= 0.4f  || joyInput.y >=  0.4f || joyInput.x <= -0.4f || joyInput.y <= -0.4f || 
		                     ((posInX + posInY) / 2) >= 0.4f) ? true : false;

		transform.position  = new Vector3 (transform.parent.position.x, transform.parent.position.y, transform.parent.position.z) - (joyInput / 2);

		if (joyPulledBack) {
			aimBow();
			prepareArrow();
			readyToFire = true;
		} else {
			if(readyToFire)	shootArrow();

			restBow();
			readyToFire = false;
		}
	}

	void restBow(){
		if (velocity.x > 0)
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(0f, 0f, 40f), (Time.deltaTime * 2));
		else if (velocity.x < 0)
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(0f, 0f, 300f), (Time.deltaTime * 2));
		else
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(0f, 0f, 0f), (Time.deltaTime * 2));
	}

	void prepareArrow(){
		if(!readyToFire)
			arrows[0] = (GameObject)Instantiate (arrow, transform.position, transform.rotation);

		arrows[0].transform.position = transform.position;
		arrows[0].transform.rotation = transform.rotation;
	}

	void shootArrow(){
		ArrowController arrowCont 	= arrows[0].GetComponent<ArrowController> ();
		Vector3 arrowVelo 			= new Vector3(-((direction.x / 2)* power), -((direction.y / 2) * power), 0);

		arrowCont.velocity 		= arrowVelo;
		arrowCont.firedAngle 	= transform.rotation.eulerAngles;
		arrowCont.isShot 		= true;
		arrowCont.collider 		= arrow.GetComponent<BoxCollider2D> ();
		arrowCont.collisionMask = 1 << 9;
	}
	
	void aimBow(){
		direction 			= (transform.position + joyInput) - transform.position;
		aimPoint.rotation  	= Quaternion.LookRotation(transform.forward, direction);
		transform.rotation 	= aimPoint.rotation;
	}
}

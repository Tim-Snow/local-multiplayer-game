using UnityEngine;
using System.Collections;

public static class Helper {

	// Rotates one object to face another object (or position)
	public static double FaceObject(Vector2 position, Vector2 target)
	{
		return (Mathf.Atan2(position.y - target.y, position.x - target.x) * (180 / Mathf.PI));
	}
	
	// Creates a Vector2 to use when moving object from position to a target, with a given speed
	public static Vector2 MoveTowards(Vector2 position, Vector2 target, float speed)
	{
		double direction = (float)(Mathf.Atan2(target.y - position.y, target.x - position.x) * 180 / Mathf.PI);
		
		Vector2 move = new Vector2(0, 0);
		
		move.x = (float)Mathf.Cos((float)direction * Mathf.PI/180) * speed;
		move.y = (float)Mathf.Sin((float)direction * Mathf.PI / 180) * speed;
		
		return move;
	}
}
